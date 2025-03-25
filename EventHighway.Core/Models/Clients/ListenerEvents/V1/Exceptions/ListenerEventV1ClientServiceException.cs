// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.ListenerEvents.V1.Exceptions
{
    public class ListenerEventV1ClientServiceException : Xeption
    {
        public ListenerEventV1ClientServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
