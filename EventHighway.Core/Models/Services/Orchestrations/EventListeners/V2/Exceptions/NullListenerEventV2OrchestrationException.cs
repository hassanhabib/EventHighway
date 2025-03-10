// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions
{
    public class NullListenerEventV2OrchestrationException : Xeption
    {
        public NullListenerEventV2OrchestrationException(string message)
            : base(message)
        { }
    }
}
