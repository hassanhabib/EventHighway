// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions
{
    public class NullEventV2OrchestrationException : Xeption
    {
        public NullEventV2OrchestrationException(string message)
            : base(message)
        { }
    }
}
