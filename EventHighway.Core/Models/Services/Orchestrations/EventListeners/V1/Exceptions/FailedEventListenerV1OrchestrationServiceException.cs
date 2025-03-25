﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions
{
    public class FailedEventListenerV1OrchestrationServiceException : Xeption
    {
        public FailedEventListenerV1OrchestrationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
