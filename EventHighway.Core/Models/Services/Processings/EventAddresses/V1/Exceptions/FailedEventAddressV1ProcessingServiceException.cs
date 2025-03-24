﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventAddresses.V1.Exceptions
{
    public class FailedEventAddressV1ProcessingServiceException : Xeption
    {
        public FailedEventAddressV1ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
