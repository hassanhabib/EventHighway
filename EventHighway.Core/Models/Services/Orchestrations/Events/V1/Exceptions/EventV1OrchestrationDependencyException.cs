// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V1.Exceptions
{
    public class EventV1OrchestrationDependencyException : Xeption
    {
        public EventV1OrchestrationDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
