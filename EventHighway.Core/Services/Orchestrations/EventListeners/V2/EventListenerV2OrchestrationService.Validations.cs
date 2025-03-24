// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal partial class EventListenerV2OrchestrationService
    {
        private static void ValidateEventListenerV2Id(Guid eventListenerV2Id)
        {
            Validate(
                (Rule: IsInvalid(eventListenerV2Id),
                Parameter: nameof(EventListenerV1.Id)));
        }

        private static void ValidateEventAddressId(Guid eventAddressId)
        {
            Validate(
                (Rule: IsInvalid(eventAddressId),
                Parameter: nameof(EventListenerV1.EventAddressId)));
        }

        private static void ValidateListenerEventV2Id(Guid listenerEventV2Id)
        {
            Validate(
                (Rule: IsInvalid(listenerEventV2Id),
                Parameter: nameof(ListenerEventV1.Id)));
        }

        private static void ValidateEventListenerV2IsNotNull(EventListenerV1 eventListenerV2)
        {
            if (eventListenerV2 is null)
            {
                throw new NullEventListenerV2OrchestrationException(
                    message: "Event listener is null.");
            }
        }

        private static void ValidateListenerEventV2IsNotNull(ListenerEventV1 listenerEventV2)
        {
            if (listenerEventV2 is null)
            {
                throw new NullListenerEventV2OrchestrationException(
                    message: "Listener event is null.");
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEventListenerV2OrchestrationException =
                new InvalidEventListenerV2OrchestrationException(
                    message: "Event listener is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEventListenerV2OrchestrationException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEventListenerV2OrchestrationException.ThrowIfContainsErrors();
        }
    }
}
