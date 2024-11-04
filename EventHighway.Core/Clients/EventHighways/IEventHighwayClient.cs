// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Clients.EventAddresses;
using EventHighway.Core.Clients.EventListeners;
using EventHighway.Core.Clients.Events;

namespace EventHighway.Core.Clients.EventHighways
{
    public interface IEventHighwayClient
    {
        public IEventAddressesClient EventAddresses { get;}
        public IEventListenersClient EventListeners { get;}
        public IEventsClient Events { get;}
    }
}
