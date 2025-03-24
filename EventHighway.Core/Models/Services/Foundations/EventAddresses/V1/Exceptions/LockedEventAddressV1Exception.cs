﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions
{
    public class LockedEventAddressV1Exception : Xeption
    {
        public LockedEventAddressV1Exception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
