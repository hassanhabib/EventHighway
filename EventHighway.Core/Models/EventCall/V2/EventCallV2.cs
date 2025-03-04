﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

namespace EventHighway.Core.Models.EventCall.V2
{
    internal class EventCallV2
    {
        public string Endpoint { get; set; }
        public string Secret { get; set; }
        public string Content { get; set; }
        public string Response { get; set; }
    }
}
