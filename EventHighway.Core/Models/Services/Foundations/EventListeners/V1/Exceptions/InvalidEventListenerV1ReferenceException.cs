﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions
{
    public class InvalidEventListenerV1ReferenceException : Xeption
    {
        public InvalidEventListenerV1ReferenceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
