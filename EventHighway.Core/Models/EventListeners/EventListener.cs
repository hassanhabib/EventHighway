// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EventHighway.Core.Models.EventAddresses;
using EventHighway.Core.Models.ListenerEvents;

namespace EventHighway.Core.Models.EventListeners
{
    public class EventListener
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeaderSecret { get; set; }
        public string Endpoint { get; set; }

        public Guid EventAddressId { get; set; }
        public EventAddress EventAddress { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public IEnumerable<ListenerEvent> ListenerEvents { get; set; }
    }
}
