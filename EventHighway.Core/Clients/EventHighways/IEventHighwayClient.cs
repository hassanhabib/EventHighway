// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Clients.EventAddresses;
using EventHighway.Core.Clients.EventAddresses.V2;
using EventHighway.Core.Clients.EventListeners;
using EventHighway.Core.Clients.EventListeners.V2;
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
        public IEventAddressV2sClient IEventAddressV2s { get; set; }
        public IEventListenerV2sClient EventListenerV2s { get; set; }
        public IListenerEventV2sClient ListenerEventV2s { get; set; }
    }
}