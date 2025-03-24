// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventAddresses.V1.Exceptions
{
    public class InvalidEventAddressV1ProcessingException : Xeption
    {
        public InvalidEventAddressV1ProcessingException(string message)
            : base(message)
        { }
    }
}
