// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;

namespace EventHighway.Core.Services.Foundations.EventAddresses.V2
{
    internal partial class EventAddressV2Service
    {
        private async ValueTask ValidateEventAddressV2OnAddAsync(EventAddressV2 eventAddressV2)
        {
            ValidateEventAddressV2IsNotNull(eventAddressV2);

            Validate(
                (Rule: IsInvalid(eventAddressV2.Id),
                Parameter: nameof(EventAddressV2.Id)),

                (Rule: IsInvalid(eventAddressV2.Name),
                Parameter: nameof(EventAddressV2.Name)),

                (Rule: IsInvalid(eventAddressV2.Description),
                Parameter: nameof(EventAddressV2.Description)),

                (Rule: IsInvalid(eventAddressV2.CreatedDate),
                Parameter: nameof(EventAddressV2.CreatedDate)),

                (Rule: IsInvalid(eventAddressV2.UpdatedDate),
                Parameter: nameof(EventAddressV2.UpdatedDate)),

                (Rule: IsNotSameAs(
                    firstDate: eventAddressV2.CreatedDate,
                    secondDate: eventAddressV2.UpdatedDate,
                    secondDateName: nameof(EventAddressV2.UpdatedDate)),

                Parameter: nameof(EventAddressV2.CreatedDate)),

                (Rule: await IsNotRecentAsync(eventAddressV2.CreatedDate),
                Parameter: nameof(EventAddressV2.CreatedDate)));
        }

        private static void ValidateEventAddressV2Id(Guid eventAddressV2Id)
        {
            Validate(
                (Rule: IsInvalid(eventAddressV2Id),
                Parameter: nameof(EventAddressV2.Id)));
        }

        private static void ValidateEventAddressV2IsNotNull(EventAddressV2 eventAddressV2)
        {
            if (eventAddressV2 is null)
            {
                throw new NullEventAddressV2Exception(
                    message: "Event address is null.");
            }
        }

        private static void ValidateEventAddressV2Exists(
            EventAddressV2 eventAddressV2,
            Guid eventAddressV2Id)
        {
            if (eventAddressV2 is null)
            {
                throw new NotFoundEventAddressV2Exception(

                    message: $"Could not find event address " +
                        $"with id: {eventAddressV2Id}.");
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Required"
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


        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEventAddressV2Exception =
                new InvalidEventAddressV2Exception(
                    message: "Event address is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEventAddressV2Exception.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEventAddressV2Exception.ThrowIfContainsErrors();
        }
    }
}
