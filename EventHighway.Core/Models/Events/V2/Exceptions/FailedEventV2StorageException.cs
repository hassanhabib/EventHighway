// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Events.V2.Exceptions
{
    public class FailedEventV2StorageException : Xeption
    {
        public FailedEventV2StorageException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
