// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventListeners.V2.Exceptions
{
    public class FailedEventListenerV2ProcessingServiceException : Xeption
    {
        public FailedEventListenerV2ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
