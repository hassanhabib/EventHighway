// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Models.Services.Processings.EventCalls.V1.Exceptions;

namespace EventHighway.Core.Services.Processings.EventCalls.V1
{
    internal partial class EventCallV1ProcessingService
    {
        private static void ValidateEventCallV1IsNotNull(EventCallV1 eventCallV1)
        {
            if (eventCallV1 is null)
            {
                throw new NullEventCallV1ProcessingException(
                    message: "Event call is null.");
            }
        }
    }
}
