// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.EventListeners.V2.Exceptions
{
    public class EventListenerV2ClientDependencyValidationException : Xeption
    {
        public EventListenerV2ClientDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
