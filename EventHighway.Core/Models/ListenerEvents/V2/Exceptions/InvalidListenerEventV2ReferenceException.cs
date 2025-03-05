// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.ListenerEvents.V2.Exceptions
{
    public class InvalidListenerEventV2ReferenceException : Xeption
    {
        public InvalidListenerEventV2ReferenceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
