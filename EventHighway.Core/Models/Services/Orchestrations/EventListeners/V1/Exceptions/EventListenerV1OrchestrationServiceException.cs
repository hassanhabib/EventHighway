// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions
{
    public class EventListenerV1OrchestrationServiceException : Xeption
    {
        public EventListenerV1OrchestrationServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
