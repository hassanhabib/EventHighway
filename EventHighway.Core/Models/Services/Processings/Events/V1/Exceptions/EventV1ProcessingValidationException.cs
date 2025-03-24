// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.Events.V1.Exceptions
{
    public class EventV1ProcessingValidationException : Xeption
    {
        public EventV1ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
