// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions
{
    public class FailedEventCallV1ServiceException : Xeption
    {
        public FailedEventCallV1ServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
