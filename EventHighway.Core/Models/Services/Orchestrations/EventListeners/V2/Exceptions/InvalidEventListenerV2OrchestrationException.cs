// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions
{
    public class InvalidEventListenerV2OrchestrationException : Xeption
    {
        public InvalidEventListenerV2OrchestrationException(string message)
            : base(message)
        { }
    }
}
