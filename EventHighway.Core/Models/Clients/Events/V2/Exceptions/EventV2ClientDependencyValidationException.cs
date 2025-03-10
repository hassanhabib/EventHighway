// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.Events.V2.Exceptions
{
    public class EventV2ClientDependencyValidationException : Xeption
    {
        public EventV2ClientDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
