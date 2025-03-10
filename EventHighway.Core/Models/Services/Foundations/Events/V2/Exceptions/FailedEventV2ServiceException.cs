// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.Events.V2.Exceptions
{
    public class FailedEventV2ServiceException : Xeption
    {
        public FailedEventV2ServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
