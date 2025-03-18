// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.Events.V2.Exceptions
{
    public class EventV2ProcessingValidationException : Xeption
    {
        public EventV2ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
