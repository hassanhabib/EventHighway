// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.Events.V1.Exceptions
{
    public class EventV1ClientDependencyException : Xeption
    {
        public EventV1ClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
