// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventAddresses.V1.Exceptions
{
    public class EventAddressV1ProcessingServiceException : Xeption
    {
        public EventAddressV1ProcessingServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
