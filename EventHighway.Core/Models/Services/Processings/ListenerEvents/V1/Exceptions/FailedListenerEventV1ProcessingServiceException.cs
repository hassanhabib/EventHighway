﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.ListenerEvents.V1.Exceptions
{
    public class FailedListenerEventV1ProcessingServiceException : Xeption
    {
        public FailedListenerEventV1ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
