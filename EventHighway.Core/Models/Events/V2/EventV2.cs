﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.EventAddresses.V2;

namespace EventHighway.Core.Models.Events.V2
{
    public class EventV2
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public EventV2Type Type { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public DateTimeOffset? ScheduledDate { get; set; }

        public Guid EventAddressId { get; set; }
        public EventAddressV2 EventAddress { get; set; }
    }
}
