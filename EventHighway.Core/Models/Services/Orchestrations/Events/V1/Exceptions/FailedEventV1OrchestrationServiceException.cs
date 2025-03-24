﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V1.Exceptions
{
    public class FailedEventV1OrchestrationServiceException : Xeption
    {
        public FailedEventV1OrchestrationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
