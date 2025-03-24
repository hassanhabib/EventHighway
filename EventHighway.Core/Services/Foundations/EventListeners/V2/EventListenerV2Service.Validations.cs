// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal partial class EventListenerV2Service
    {
        private async ValueTask ValidateEventListenerV2OnAddAsync(EventListenerV1 eventListenerV2)
        {
            ValidateEventListenerV2IsNotNull(eventListenerV2);

            Validate(
                (Rule: IsInvalid(eventListenerV2.Id),
                Parameter: nameof(EventListenerV1.Id)),

                (Rule: IsInvalid(eventListenerV2.Name),
                Parameter: nameof(EventListenerV1.Name)),

                (Rule: IsInvalid(eventListenerV2.Description),
                Parameter: nameof(EventListenerV1.Description)),

                (Rule: IsInvalid(eventListenerV2.HeaderSecret),
                Parameter: nameof(EventListenerV1.HeaderSecret)),

                (Rule: IsInvalid(eventListenerV2.Endpoint),
                Parameter: nameof(EventListenerV1.Endpoint)),

                (Rule: IsInvalid(eventListenerV2.EventAddressId),
                Parameter: nameof(EventListenerV1.EventAddressId)),

                (Rule: IsInvalid(eventListenerV2.CreatedDate),
                Parameter: nameof(EventListenerV1.CreatedDate)),

                (Rule: IsInvalid(eventListenerV2.UpdatedDate),
                Parameter: nameof(EventListenerV1.UpdatedDate)),

                (Rule: IsNotSameAs(
                    firstDate: eventListenerV2.CreatedDate,
                    secondDate: eventListenerV2.UpdatedDate,
                    secondDateName: nameof(EventListenerV1.UpdatedDate)),

                Parameter: nameof(EventListenerV1.CreatedDate)),

                (Rule: await IsNotRecentAsync(eventListenerV2.CreatedDate),
                Parameter: nameof(EventListenerV1.CreatedDate)));
        }

        private static void ValidateEventListenerV2Id(Guid eventListenerV2Id)
        {
            Validate(
                (Rule: IsInvalid(eventListenerV2Id),
                Parameter: nameof(EventListenerV1.Id)));
        }

        private static void ValidateEventListenerV2Exists(
            EventListenerV1 eventListenerV2,
            Guid eventListenerV2Id)
        {
            if (eventListenerV2 is null)
            {
                throw new NotFoundEventListenerV2Exception(

                    message: $"Could not find event listener " +
                        $"with id: {eventListenerV2Id}.");
            }
        }

        private static void ValidateEventListenerV2IsNotNull(EventListenerV1 eventListenerV2)
        {
            if (eventListenerV2 is null)
            {
                throw new NullEventListenerV2Exception(
                    message: "Event listener is null.");
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(value: text),
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
            var invalidEventListenerV2Exception =
                new InvalidEventListenerV2Exception(
                    message: "Event listener is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEventListenerV2Exception.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEventListenerV2Exception.ThrowIfContainsErrors();
        }
    }
}
