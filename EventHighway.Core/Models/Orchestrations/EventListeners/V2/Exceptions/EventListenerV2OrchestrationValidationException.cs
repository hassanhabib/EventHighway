// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Orchestrations.EventListeners.V2.Exceptions
{
    public class EventListenerV2OrchestrationValidationException : Xeption
    {
        public EventListenerV2OrchestrationValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
