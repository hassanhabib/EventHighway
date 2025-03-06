// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.Processings.EventListeners.V2.Exceptions;

namespace EventHighway.Core.Services.Processings.EventListeners.V2
{
    internal partial class EventListenerV2ProcessingService
    {
        private static void ValidateEventAddressId(Guid eventAddressId)
        {
            Validate(
                (Rule: IsInvalid(eventAddressId),
                Parameter: nameof(EventListenerV2.EventAddressId)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEventListenerV2ProcessingException =
                new InvalidEventListenerV2ProcessingException(
                    message: "Event listener is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEventListenerV2ProcessingException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEventListenerV2ProcessingException.ThrowIfContainsErrors();
        }
    }
}
