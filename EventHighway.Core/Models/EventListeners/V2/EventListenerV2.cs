// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.EventAddresses.V2;

namespace EventHighway.Core.Models.EventListeners.V2
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
        public EventAddressV2 EventAddress { get; set; }
    }
}
