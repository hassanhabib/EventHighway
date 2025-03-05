// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace EventHighway.Core.Models.EventCall.V2.Exceptions
{
    public class InvalidEventCallV2Exception : Xeption
    {
        public InvalidEventCallV2Exception(string message)
            : base(message)
        { }

        public InvalidEventCallV2Exception(string message, Exception innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
