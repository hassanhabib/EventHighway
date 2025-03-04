// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.EventAddresses.V2;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.Events.V2;

namespace EventHighway.Core.Models.ListenerEvents.V2
{
    public class ListenerEventV2
    {
        public Guid Id { get; set; }
        public ListenerEventV2Status Status { get; set; }
        public string Response { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid EventId { get; set; }
        public EventV2 Event { get; set; }

        public Guid EventAddressId { get; set; }
        public EventAddressV2 EventAddress { get; set; }

        public Guid EventListenerId { get; set; }
        public EventListenerV2 EventListener { get; set; }
    }
}
