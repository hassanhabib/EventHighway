// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.EventListeners.V1.Exceptions
{
    public class EventListenerV1ClientServiceException : Xeption
    {
        public EventListenerV1ClientServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
