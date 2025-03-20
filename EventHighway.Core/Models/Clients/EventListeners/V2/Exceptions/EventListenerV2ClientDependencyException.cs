// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.EventListeners.V2.Exceptions
{
    public class EventListenerV2ClientDependencyException : Xeption
    {
        public EventListenerV2ClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
