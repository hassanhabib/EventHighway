// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.EventCall.V2.Exceptions;

namespace EventHighway.Core.Services.Foundations.EventCalls.V2
{
    internal partial class EventCallV2Service
    {
        private void ValidateEventCallV2OnRun(EventCallV2 eventCallV2)
        {
            ValidateEventCallV2IsNotNull(eventCallV2);

            Validate(
                (Rule: IsInvalid(eventCallV2.Endpoint),
                Parameter: nameof(EventCallV2.Endpoint)),

                (Rule: IsInvalid(eventCallV2.Content),
                Parameter: nameof(EventCallV2.Content)));
        }

        private static void ValidateEventCallV2IsNotNull(EventCallV2 eventCallV2)
        {
            if (eventCallV2 is null)
            {
                throw new NullEventCallV2Exception(
                    message: "Event call is null.");
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEventCallV2Exception =
                new InvalidEventCallV2Exception(
                    message: "Event call is invalid, fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEventCallV2Exception.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEventCallV2Exception.ThrowIfContainsErrors();
        }
    }
}
