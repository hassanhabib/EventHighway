// ---------------------------------------------------------------
// Copyright (c) Aspen Publishing. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Processings.Events.V2.Exceptions
{
    public class FailedEventV2ProcessingServiceException : Xeption
    {
        public FailedEventV2ProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
