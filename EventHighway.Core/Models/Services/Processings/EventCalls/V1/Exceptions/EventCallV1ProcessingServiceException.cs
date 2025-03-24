// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventCalls.V1.Exceptions
{
    public class EventCallV1ProcessingServiceException : Xeption
    {
        public EventCallV1ProcessingServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
