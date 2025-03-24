// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventCalls.V1.Exceptions
{
    public class EventCallV1ProcessingDependencyException : Xeption
    {
        public EventCallV1ProcessingDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
