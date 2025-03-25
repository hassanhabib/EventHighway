// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions
{
    public class InvalidEventListenerV1OrchestrationException : Xeption
    {
        public InvalidEventListenerV1OrchestrationException(string message)
            : base(message)
        { }
    }
}
