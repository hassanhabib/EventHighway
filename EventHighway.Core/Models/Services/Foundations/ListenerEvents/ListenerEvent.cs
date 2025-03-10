// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Services.Foundations.EventAddresses;
using EventHighway.Core.Models.Services.Foundations.EventListeners;
using EventHighway.Core.Models.Services.Foundations.Events;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents
{
    public class ListenerEvent
    {
        public Guid Id { get; set; }
        public ListenerEventStatus Status { get; set; }
        public string Response { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid EventAddressId { get; set; }
        public EventAddress EventAddress { get; set; }

        public Guid EventListenerId { get; set; }
        public EventListener EventListener { get; set; }
    }
}
