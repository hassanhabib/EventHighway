// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventAddresses.V2.Exceptions
{
    public class FailedEventAddressV2ProcessingServiceException : Xeption
    {
        public FailedEventAddressV2ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
