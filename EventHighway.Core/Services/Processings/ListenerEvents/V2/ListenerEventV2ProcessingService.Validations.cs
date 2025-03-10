// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions;

namespace EventHighway.Core.Services.Processings.ListenerEvents.V2
{
    internal partial class ListenerEventV2ProcessingService
    {
        private static void ValidateListenerEventV2IsNotNull(ListenerEventV2 listenerEventV2)
        {
            if (listenerEventV2 is null)
            {
                throw new NullListenerEventV2ProcessingException(
                    message: "Listener event is null.");
            }
        }
    }
}
