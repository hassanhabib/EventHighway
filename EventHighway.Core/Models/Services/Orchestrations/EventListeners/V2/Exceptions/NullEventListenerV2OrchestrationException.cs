// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions
{
    public class NullEventListenerV2OrchestrationException : Xeption
    {
        public NullEventListenerV2OrchestrationException(string message)
            : base(message)
        { }
    }
}
