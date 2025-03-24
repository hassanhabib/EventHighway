// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class AlreadyExistsListenerEventV2Exception : Xeption
    {
        public AlreadyExistsListenerEventV2Exception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
