// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;

namespace EventHighway.Core.Services.Foundations.EventAddresses.V2
{
    internal partial class EventAddressV2Service
    {
        private static void ValidateEventAddressV2Id(Guid eventAddressV2Id)
        {
            Validate(
                (Rule: IsInvalid(eventAddressV2Id),
                Parameter: nameof(EventAddressV2.Id)));
        }

        private static void ValidateEventAddressV2IsNotNull(EventAddressV2 eventV2)
        {
            if (eventV2 is null)
            {
                throw new NullEventAddressV2Exception(
                    message: "Event address is null.");
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Required"
        };

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
