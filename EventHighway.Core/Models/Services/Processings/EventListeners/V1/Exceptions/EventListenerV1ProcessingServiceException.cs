// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventListeners.V1.Exceptions
{
    public class EventListenerV1ProcessingServiceException : Xeption
    {
        public EventListenerV1ProcessingServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
