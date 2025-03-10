// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions
{
    public class FailedEventV2OrchestrationServiceException : Xeption
    {
        public FailedEventV2OrchestrationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
