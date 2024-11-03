﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.EventAddresses;

namespace EventHighway.Core.Models.Events
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        
        public Guid EventAddressId { get; set; }
        public EventAddress EventAddress { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}