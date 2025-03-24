// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class LockedListenerEventV2Exception : Xeption
    {
        public LockedListenerEventV2Exception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
