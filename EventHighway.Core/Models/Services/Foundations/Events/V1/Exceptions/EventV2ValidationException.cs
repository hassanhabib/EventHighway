// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.Events.V1.Exceptions
{
    public class EventV2ValidationException : Xeption
    {
        public EventV2ValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
