// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions
{
    public class InvalidEventCallV1Exception : Xeption
    {
        public InvalidEventCallV1Exception(string message)
            : base(message)
        { }

        public InvalidEventCallV1Exception(string message, Exception innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
