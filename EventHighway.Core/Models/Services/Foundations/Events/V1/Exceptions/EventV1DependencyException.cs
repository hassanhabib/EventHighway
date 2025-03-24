// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.Events.V1.Exceptions
{
    public class EventV1DependencyException : Xeption
    {
        public EventV1DependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
