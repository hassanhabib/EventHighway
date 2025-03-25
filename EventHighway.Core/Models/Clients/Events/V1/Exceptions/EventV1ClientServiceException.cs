// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.Events.V1.Exceptions
{
    public class EventV1ClientServiceException : Xeption
    {
        public EventV1ClientServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
