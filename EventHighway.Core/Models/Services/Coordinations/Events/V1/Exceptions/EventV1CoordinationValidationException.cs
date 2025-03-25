// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Coordinations.Events.V1.Exceptions
{
    public class EventV1CoordinationValidationException : Xeption
    {
        public EventV1CoordinationValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
