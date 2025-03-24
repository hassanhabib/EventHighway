// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions
{
    internal class EventListenerV2ServiceException : Xeption
    {
        public EventListenerV2ServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
