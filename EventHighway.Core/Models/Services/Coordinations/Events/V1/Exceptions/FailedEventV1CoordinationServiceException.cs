// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Coordinations.Events.V1.Exceptions
{
    public class FailedEventV1CoordinationServiceException : Xeption
    {
        public FailedEventV1CoordinationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
