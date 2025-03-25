// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.ListenerEvents.V1.Exceptions
{
    public class ListenerEventV1ClientDependencyException : Xeption
    {
        public ListenerEventV1ClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
