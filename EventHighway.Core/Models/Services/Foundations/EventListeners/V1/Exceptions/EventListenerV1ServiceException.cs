// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions
{
    internal class EventListenerV1ServiceException : Xeption
    {
        public EventListenerV1ServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
