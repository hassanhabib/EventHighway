// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.EventCalls.V2.Exceptions
{
    public class EventCallV2ProcessingServiceException : Xeption
    {
        public EventCallV2ProcessingServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
