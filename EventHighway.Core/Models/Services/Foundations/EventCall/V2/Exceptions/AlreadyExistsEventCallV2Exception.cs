// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V2.Exceptions
{
    public class AlreadyExistsEventCallV2Exception : Xeption
    {
        public AlreadyExistsEventCallV2Exception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
