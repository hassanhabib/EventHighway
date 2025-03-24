// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V1.Exceptions
{
    public class EventV1OrchestrationServiceException : Xeption
    {
        public EventV1OrchestrationServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
