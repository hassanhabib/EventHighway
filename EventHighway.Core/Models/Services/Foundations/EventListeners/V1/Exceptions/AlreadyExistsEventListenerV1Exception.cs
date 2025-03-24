// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions
{
    public class AlreadyExistsEventListenerV1Exception : Xeption
    {
        public AlreadyExistsEventListenerV1Exception(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
