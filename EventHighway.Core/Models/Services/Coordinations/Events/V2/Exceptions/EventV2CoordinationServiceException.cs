// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions
{
    public class EventV2CoordinationServiceException : Xeption
    {
        public EventV2CoordinationServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
