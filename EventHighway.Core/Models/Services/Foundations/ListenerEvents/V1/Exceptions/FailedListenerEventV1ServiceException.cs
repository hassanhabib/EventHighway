﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class FailedListenerEventV1ServiceException : Xeption
    {
        public FailedListenerEventV1ServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
