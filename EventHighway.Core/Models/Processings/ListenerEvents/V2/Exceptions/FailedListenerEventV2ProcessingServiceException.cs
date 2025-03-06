// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Processings.ListenerEvents.V2.Exceptions
{
    public class FailedListenerEventV2ProcessingServiceException : Xeption
    {
        public FailedListenerEventV2ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
