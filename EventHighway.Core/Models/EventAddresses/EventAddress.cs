// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EventHighway.Core.Models.EventListeners;
using EventHighway.Core.Models.Events;

namespace EventHighway.Core.Models.EventAddresses
{
    public class EventAddress
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<EventListener> EventListeners { get; set; }
    }
}
