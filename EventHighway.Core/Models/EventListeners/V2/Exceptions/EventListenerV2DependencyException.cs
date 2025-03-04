// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.EventListeners.V2.Exceptions
{
    public class EventListenerV2DependencyException : Xeption
    {
        public EventListenerV2DependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
