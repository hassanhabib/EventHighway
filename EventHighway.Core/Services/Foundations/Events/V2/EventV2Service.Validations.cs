// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1.Exceptions;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial class EventV2Service
    {
        private async ValueTask ValidateEventV2OnAddAsync(EventV1 eventV2)
        {
            ValidateEventV2IsNotNull(eventV2);

            Validate(
                (Rule: IsInvalid(eventV2.Id),
                Parameter: nameof(EventV1.Id)),

                (Rule: IsInvalid(eventV2.Content),
                Parameter: nameof(EventV1.Content)),

                (Rule: IsInvalid(eventV2.EventAddressId),
                Parameter: nameof(EventV1.EventAddressId)),

                (Rule: IsInvalid(eventV2.Type),
                Parameter: nameof(EventV1.Type)),

                (Rule: IsInvalid(eventV2.CreatedDate),
                Parameter: nameof(EventV1.CreatedDate)),

                (Rule: IsInvalid(eventV2.UpdatedDate),
                Parameter: nameof(EventV1.UpdatedDate)),

                (Rule: IsNotSameAs(
                    firstDate: eventV2.CreatedDate,
                    secondDate: eventV2.UpdatedDate,
                    secondDateName: nameof(EventV1.UpdatedDate)),

                Parameter: nameof(EventV1.CreatedDate)),

                (Rule: await IsNotRecentAsync(eventV2.CreatedDate),
                Parameter: nameof(EventV1.CreatedDate)));
        }

        private static void ValidateEventV2Id(Guid eventV2Id)
        {
            Validate(
                (Rule: IsInvalid(eventV2Id),
                Parameter: nameof(EventV1.Id)));
        }

        private static void ValidateEventV2IsNotNull(EventV1 eventV2)
        {
            if (eventV2 is null)
            {
                throw new NullEventV2Exception(
                    message: "Event is null.");
            }
        }

        private static void ValidateEventV2Exists(
            EventV1 eventV2,
            Guid eventV2Id)
        {
            if (eventV2 is null)
            {
                throw new NotFoundEventV2Exception(

                    message: $"Could not find event " +
                        $"with id: {eventV2Id}.");
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Required"
        };

        private static dynamic IsInvalid<T>(T value) => new
        {
            Condition = IsInvalidEnum(value) is true,
            Message = "Value is not recognized"
        };

        private static dynamic IsNotSameAs(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private async ValueTask<dynamic> IsNotRecentAsync(DateTimeOffset date) => new
        {
            Condition = await IsDateNotRecentAsync(date),
            Message = "Date is not recent"
        };

        private async ValueTask<bool> IsDateNotRecentAsync(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                await this.dateTimeBroker.GetDateTimeOffsetAsync();

            TimeSpan timeDifference = currentDateTime.Subtract(value: date);

            return timeDifference.TotalSeconds is > 60 or < 0;
        }

        private static bool IsInvalidEnum<T>(T enumValue)
        {
            bool isDefined = Enum.IsDefined(
                enumType: typeof(T),
                value: enumValue);

            return isDefined is false;
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEventV2Exception =
                new InvalidEventV2Exception(
                    message: "Event is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEventV2Exception.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEventV2Exception.ThrowIfContainsErrors();
        }
    }
}
