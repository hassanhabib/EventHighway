﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventListeners.V1.Exceptions
{
    public class FailedEventListenerV1ProcessingServiceException : Xeption
    {
        public FailedEventListenerV1ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
