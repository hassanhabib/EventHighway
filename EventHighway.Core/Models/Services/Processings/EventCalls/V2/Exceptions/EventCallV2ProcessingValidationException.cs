// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventCalls.V2.Exceptions
{
    public class EventCallV2ProcessingValidationException : Xeption
    {
        public EventCallV2ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
