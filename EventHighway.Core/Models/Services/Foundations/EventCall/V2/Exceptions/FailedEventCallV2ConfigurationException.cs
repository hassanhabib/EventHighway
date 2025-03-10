// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V2.Exceptions
{
    public class FailedEventCallV2ConfigurationException : Xeption
    {
        public FailedEventCallV2ConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
