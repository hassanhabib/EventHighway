// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V1.Exceptions
{
    public class EventV1OrchestrationValidationException : Xeption
    {
        public EventV1OrchestrationValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
