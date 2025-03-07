// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Orchestrations.Events.V2.Exceptions
{
    public class NullEventCallV2OrchestrationException : Xeption
    {
        public NullEventCallV2OrchestrationException(string message)
            : base(message)
        { }
    }
}
