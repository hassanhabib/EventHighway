// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventAddresses.V2.Exceptions
{
    public class EventAddressV2ProcessingValidationException : Xeption
    {
        public EventAddressV2ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
