// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V2.Exceptions
{
    public class FailedEventCallV2RequestException : Xeption
    {
        public FailedEventCallV2RequestException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
