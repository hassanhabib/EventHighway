// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventCalls.V1.Exceptions
{
    public class EventCallV1ProcessingValidationException : Xeption
    {
        public EventCallV1ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
