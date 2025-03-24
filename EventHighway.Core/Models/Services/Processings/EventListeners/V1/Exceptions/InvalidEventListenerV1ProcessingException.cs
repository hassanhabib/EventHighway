// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventListeners.V1.Exceptions
{
    public class InvalidEventListenerV1ProcessingException : Xeption
    {
        public InvalidEventListenerV1ProcessingException(string message)
            : base(message)
        { }
    }
}
