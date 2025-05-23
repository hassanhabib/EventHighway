﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions
{
    public class NullEventCallV1Exception : Xeption
    {
        public NullEventCallV1Exception(string message)
            : base(message)
        { }
    }
}
