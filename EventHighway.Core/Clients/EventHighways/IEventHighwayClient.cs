// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Clients.EventAddresses;
using EventHighway.Core.Clients.EventAddresses.V1;
using EventHighway.Core.Clients.EventListeners;
using EventHighway.Core.Clients.EventListeners.V1;
using EventHighway.Core.Clients.Events;
using EventHighway.Core.Clients.Events.V2;
using EventHighway.Core.Clients.ListenerEvents.V2;

namespace EventHighway.Core.Clients.EventHighways
{
    public interface IEventHighwayClient
    {
        public IEventAddressesClient EventAddresses { get; }
        public IEventListenersClient EventListeners { get; }
        public IEventsClient Events { get; }
        public IEventV2sClient EventV2s { get; set; }
        public IEventAddressesV1Client IEventAddressV1s { get; set; }
        public IEventListenerV1sClient EventListenerV1s { get; set; }
        public IListenerEventV2sClient ListenerEventV2s { get; set; }
    }
}