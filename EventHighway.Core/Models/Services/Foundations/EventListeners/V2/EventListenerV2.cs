// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V2
{
    public class EventListenerV2
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HeaderSecret { get; set; }
        public string Endpoint { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid EventAddressId { get; set; }
        public EventAddressV1 EventAddress { get; set; }

        public IEnumerable<ListenerEventV2> ListenerEvents { get; set; }
    }
}
