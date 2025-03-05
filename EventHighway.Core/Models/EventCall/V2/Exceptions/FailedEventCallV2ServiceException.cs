// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.EventCall.V2.Exceptions
{
    public class FailedEventCallV2ServiceException : Xeption
    {
        public FailedEventCallV2ServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
