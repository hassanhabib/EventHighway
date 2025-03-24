// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions
{
    public class FailedEventAddressV2StorageException : Xeption
    {
        public FailedEventAddressV2StorageException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
