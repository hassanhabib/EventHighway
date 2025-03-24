// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions
{
    public class EventListenerV1DependencyValidationException : Xeption
    {
        public EventListenerV1DependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
