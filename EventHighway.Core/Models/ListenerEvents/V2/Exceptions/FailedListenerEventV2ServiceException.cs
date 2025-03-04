// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.ListenerEvents.V2.Exceptions
{
    public class FailedListenerEventV2ServiceException : Xeption
    {
        public FailedListenerEventV2ServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
