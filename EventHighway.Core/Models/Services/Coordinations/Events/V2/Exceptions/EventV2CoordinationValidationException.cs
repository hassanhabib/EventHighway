// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions
{
    public class EventV2CoordinationValidationException : Xeption
    {
        public EventV2CoordinationValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
