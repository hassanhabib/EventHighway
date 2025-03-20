// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2ClientDependencyValidationException : Xeption
    {
        public ListenerEventV2ClientDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
