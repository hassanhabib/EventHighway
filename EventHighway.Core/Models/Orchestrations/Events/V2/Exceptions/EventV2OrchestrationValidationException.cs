// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Orchestrations.Events.V2.Exceptions
{
    public class EventV2OrchestrationValidationException : Xeption
    {
        public EventV2OrchestrationValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
