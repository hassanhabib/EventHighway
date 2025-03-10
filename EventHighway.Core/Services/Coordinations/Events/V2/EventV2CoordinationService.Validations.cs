// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.ListenerEvents.V2;

namespace EventHighway.Core.Services.Coordinations.Events.V2
{
    internal partial class EventV2CoordinationService
    {
        private static void ValidateListenerEventV2IsNotNull(ListenerEventV2 listenerEventV2)
        {
            if (listenerEventV2 is null)
            {
                throw new NullListenerEventV2CoordinationException(
                    message: "Listener event is null.");
            }
        }
    }
}
