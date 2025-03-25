// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.EventListeners.V1.Exceptions
{
    public class EventListenerV1ClientDependencyException : Xeption
    {
        public EventListenerV1ClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
