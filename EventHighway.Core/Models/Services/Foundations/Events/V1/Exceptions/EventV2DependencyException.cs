// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.Events.V1.Exceptions
{
    public class EventV2DependencyException : Xeption
    {
        public EventV2DependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
