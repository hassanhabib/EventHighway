// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.Events.V2.Exceptions
{
    public class FailedEventV2ProcessingServiceException : Xeption
    {
        public FailedEventV2ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
