// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V1.Exceptions
{
    public class NullEventV1OrchestrationException : Xeption
    {
        public NullEventV1OrchestrationException(string message)
            : base(message)
        { }
    }
}
