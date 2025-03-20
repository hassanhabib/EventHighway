// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2ClientDependencyException : Xeption
    {
        public ListenerEventV2ClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
