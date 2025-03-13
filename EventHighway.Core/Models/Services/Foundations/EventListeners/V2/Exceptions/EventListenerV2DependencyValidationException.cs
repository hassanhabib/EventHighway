// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions
{
    public class EventListenerV2DependencyValidationException : Xeption
    {
        public EventListenerV2DependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
