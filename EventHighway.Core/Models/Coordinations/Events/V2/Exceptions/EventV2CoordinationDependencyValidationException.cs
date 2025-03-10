// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Coordinations.Events.V2.Exceptions
{
    public class EventV2CoordinationDependencyValidationException : Xeption
    {
        public EventV2CoordinationDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
