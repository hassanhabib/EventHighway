// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.Events.V2.Exceptions
{
    public class EventV2ClientServiceException : Xeption
    {
        public EventV2ClientServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
