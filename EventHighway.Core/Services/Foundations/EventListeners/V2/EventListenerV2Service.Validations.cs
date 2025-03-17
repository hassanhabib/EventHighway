// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal partial class EventListenerV2Service
    {
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
