// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions
{
    public class EventListenerV1ValidationException : Xeption
    {
        public EventListenerV1ValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
