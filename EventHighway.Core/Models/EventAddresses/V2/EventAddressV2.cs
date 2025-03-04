// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.ListenerEvents.V2;

namespace EventHighway.Core.Models.EventAddresses.V2
{
    public class EventAddressV2
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public IEnumerable<EventV2> Events { get; set; }
        public IEnumerable<EventListenerV2> EventListeners { get; set; }
        public IEnumerable<ListenerEventV2> ListenerEvents { get; set; }
    }
}
