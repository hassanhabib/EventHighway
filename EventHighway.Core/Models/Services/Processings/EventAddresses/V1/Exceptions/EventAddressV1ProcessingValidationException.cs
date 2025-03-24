// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventAddresses.V1.Exceptions
{
    public class EventAddressV1ProcessingValidationException : Xeption
    {
        public EventAddressV1ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
