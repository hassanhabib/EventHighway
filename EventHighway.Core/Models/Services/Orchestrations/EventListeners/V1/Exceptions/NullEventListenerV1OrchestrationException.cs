// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions
{
    public class NullEventListenerV1OrchestrationException : Xeption
    {
        public NullEventListenerV1OrchestrationException(string message)
            : base(message)
        { }
    }
}
