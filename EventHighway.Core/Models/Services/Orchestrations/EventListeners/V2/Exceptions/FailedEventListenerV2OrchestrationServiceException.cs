// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions
{
    public class FailedEventListenerV2OrchestrationServiceException : Xeption
    {
        public FailedEventListenerV2OrchestrationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
