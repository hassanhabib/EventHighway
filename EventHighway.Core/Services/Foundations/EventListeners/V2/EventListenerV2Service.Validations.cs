// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal partial class EventListenerV2Service
    {
        private async ValueTask ValidateEventListenerV2OnAddAsync(EventListenerV2 eventListenerV2)
        {
            ValidateEventListenerV2IsNotNull(eventListenerV2);

            Validate(
                (Rule: IsInvalid(eventListenerV2.Id),
                Parameter: nameof(EventListenerV2.Id)),

                (Rule: IsInvalid(eventListenerV2.Name),
                Parameter: nameof(EventListenerV2.Name)),

                (Rule: IsInvalid(eventListenerV2.Description),
                Parameter: nameof(EventListenerV2.Description)),

                (Rule: IsInvalid(eventListenerV2.HeaderSecret),
                Parameter: nameof(EventListenerV2.HeaderSecret)),

                (Rule: IsInvalid(eventListenerV2.Endpoint),
                Parameter: nameof(EventListenerV2.Endpoint)),

                (Rule: IsInvalid(eventListenerV2.EventAddressId),
                Parameter: nameof(EventListenerV2.EventAddressId)),

                (Rule: IsInvalid(eventListenerV2.CreatedDate),
                Parameter: nameof(EventListenerV2.CreatedDate)),

                (Rule: IsInvalid(eventListenerV2.UpdatedDate),
                Parameter: nameof(EventListenerV2.UpdatedDate)),

                (Rule: IsNotSameAs(
                    firstDate: eventListenerV2.CreatedDate,
                    secondDate: eventListenerV2.UpdatedDate,
                    secondDateName: nameof(EventListenerV2.UpdatedDate)),

                Parameter: nameof(EventListenerV2.CreatedDate)));
        }

        private static void ValidateEventListenerV2Id(Guid eventListenerV2Id)
        {
            Validate(
                (Rule: IsInvalid(eventListenerV2Id),
                Parameter: nameof(EventListenerV2.Id)));
        }

        private static void ValidateEventListenerV2Exists(
            EventListenerV2 eventListenerV2,
            Guid eventListenerV2Id)
        {
            if (eventListenerV2 is null)
            {
                throw new NotFoundEventListenerV2Exception(

                    message: $"Could not find event listener " +
                        $"with id: {eventListenerV2Id}.");
            }
        }

        private static void ValidateEventListenerV2IsNotNull(EventListenerV2 eventListenerV2)
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
            Condition = string.IsNullOrWhiteSpace(text),
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
