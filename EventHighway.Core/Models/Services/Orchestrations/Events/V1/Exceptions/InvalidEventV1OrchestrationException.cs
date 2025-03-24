// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V1.Exceptions
{
    public class InvalidEventV1OrchestrationException : Xeption
    {
        public InvalidEventV1OrchestrationException(string message)
            : base(message)
        { }
    }
}
