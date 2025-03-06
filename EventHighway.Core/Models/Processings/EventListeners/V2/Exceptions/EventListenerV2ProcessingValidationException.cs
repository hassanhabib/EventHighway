// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.EventListeners.V2.Exceptions
{
    public class EventListenerV2ProcessingValidationException : Xeption
    {
        public EventListenerV2ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
