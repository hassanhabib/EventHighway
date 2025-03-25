// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.EventListeners.V1.Exceptions
{
    public class EventListenerV1ClientDependencyValidationException : Xeption
    {
        public EventListenerV1ClientDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
