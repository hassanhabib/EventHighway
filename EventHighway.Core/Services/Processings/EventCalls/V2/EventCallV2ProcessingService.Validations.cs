// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.Services.Processings.EventCalls.V2.Exceptions;

namespace EventHighway.Core.Services.Processings.EventCalls.V2
{
    internal partial class EventCallV2ProcessingService
    {
        private static void ValidateEventCallV2IsNotNull(EventCallV2 eventCallV2)
        {
            if (eventCallV2 is null)
            {
                throw new NullEventCallV2ProcessingException(
                    message: "Event call is null.");
            }
        }
    }
}
