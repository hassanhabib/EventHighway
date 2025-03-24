﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventCalls.V1.Exceptions
{
    public class FailedEventCallV1ProcessingServiceException : Xeption
    {
        public FailedEventCallV1ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
