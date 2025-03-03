// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

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
    }
}
