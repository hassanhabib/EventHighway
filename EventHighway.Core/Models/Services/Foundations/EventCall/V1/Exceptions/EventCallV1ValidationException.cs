// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions
{
    public class EventCallV1ValidationException : Xeption
    {
        public EventCallV1ValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
