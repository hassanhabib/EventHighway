// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2ClientServiceException : Xeption
    {
        public ListenerEventV2ClientServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
