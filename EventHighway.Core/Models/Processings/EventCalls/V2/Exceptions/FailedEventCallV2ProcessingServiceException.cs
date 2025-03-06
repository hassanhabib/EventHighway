// ---------------------------------------------------------------
// Copyright (c) Aspen Publishing. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Processings.EventCalls.V2.Exceptions
{
    public class FailedEventCallV2ProcessingServiceException : Xeption
    {
        public FailedEventCallV2ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
