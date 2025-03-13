// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.Events.V2.Exceptions
{
    public class LockedEventV2Exception : Xeption
    {
        public LockedEventV2Exception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
