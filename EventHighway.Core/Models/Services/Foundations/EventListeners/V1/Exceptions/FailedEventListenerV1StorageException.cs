// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions
{
    public class FailedEventListenerV1StorageException : Xeption
    {
        public FailedEventListenerV1StorageException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
