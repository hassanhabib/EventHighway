﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Events.V2.Exceptions
{
    public class EventV2DependencyValidationException : Xeption
    {
        public EventV2DependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
