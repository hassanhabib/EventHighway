// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions
{
    public class InvalidEventCallV2ReferenceException : Xeption
    {
        public InvalidEventCallV2ReferenceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
