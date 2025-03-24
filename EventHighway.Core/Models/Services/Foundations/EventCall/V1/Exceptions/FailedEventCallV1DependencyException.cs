// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions
{
    public class FailedEventCallV1DependencyException : Xeption
    {
        public FailedEventCallV1DependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
