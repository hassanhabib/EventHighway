// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions
{
    public class AlreadyExistsEventListenerV2Exception : Xeption
    {
        public AlreadyExistsEventListenerV2Exception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
