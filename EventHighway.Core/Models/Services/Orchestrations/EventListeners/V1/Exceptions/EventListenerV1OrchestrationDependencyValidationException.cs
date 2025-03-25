// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions
{
    public class EventListenerV1OrchestrationDependencyValidationException : Xeption
    {
        public EventListenerV1OrchestrationDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
