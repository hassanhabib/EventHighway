// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V2.Exceptions
{
    public class EventCallV2ValidationException : Xeption
    {
        public EventCallV2ValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
