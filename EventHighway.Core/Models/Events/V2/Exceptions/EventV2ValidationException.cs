// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Events.V2.Exceptions
{
    public class EventV2ValidationException : Xeption
    {
        public EventV2ValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
