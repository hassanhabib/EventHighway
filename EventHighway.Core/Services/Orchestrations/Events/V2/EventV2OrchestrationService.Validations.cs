// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions;

namespace EventHighway.Core.Services.Orchestrations.Events.V2
{
    internal partial class EventV2OrchestrationService
    {
        private static void ValidateEventCallV2IsNotNull(EventCallV2 eventCallV2)
        {
            if (eventCallV2 is null)
            {
                throw new NullEventCallV2OrchestrationException(
                    message: "Event call is null.");
            }
        }

        private static void ValidateEventV2IsNotNull(EventV2 eventV2)
        {
            if (eventV2 is null)
            {
                throw new NullEventV2OrchestrationException(
                    message: "Event is null.");
            }
        }

        private static void ValidateEventV2Id(Guid eventV2Id)
        {
            Validate(
                (Rule: IsInvalid(eventV2Id),
                Parameter: nameof(EventV2.Id)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEventV2OrchestrationException =
                new InvalidEventV2OrchestrationException(
                    message: "Event is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEventV2OrchestrationException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEventV2OrchestrationException.ThrowIfContainsErrors();
        }
    }
}
