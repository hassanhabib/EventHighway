// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions
{
    public class FailedEventV2CoordinationServiceException : Xeption
    {
        public FailedEventV2CoordinationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
