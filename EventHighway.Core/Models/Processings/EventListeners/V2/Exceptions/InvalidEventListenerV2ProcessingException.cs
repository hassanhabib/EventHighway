// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.EventListeners.V2.Exceptions
{
    public class InvalidEventListenerV2ProcessingException : Xeption
    {
        public InvalidEventListenerV2ProcessingException(string message)
            : base(message)
        { }
    }
}
