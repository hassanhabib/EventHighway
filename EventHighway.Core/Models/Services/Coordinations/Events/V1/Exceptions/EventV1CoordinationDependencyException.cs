// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Coordinations.Events.V1.Exceptions
{
    public class EventV1CoordinationDependencyException : Xeption
    {
        public EventV1CoordinationDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
