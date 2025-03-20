// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.EventAddresses.V2.Exceptions
{
    public class EventAddressV2ClientServiceException : Xeption
    {
        public EventAddressV2ClientServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
