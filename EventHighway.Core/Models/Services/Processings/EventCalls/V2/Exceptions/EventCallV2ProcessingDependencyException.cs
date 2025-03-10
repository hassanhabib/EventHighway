// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventCalls.V2.Exceptions
{
    public class EventCallV2ProcessingDependencyException : Xeption
    {
        public EventCallV2ProcessingDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
