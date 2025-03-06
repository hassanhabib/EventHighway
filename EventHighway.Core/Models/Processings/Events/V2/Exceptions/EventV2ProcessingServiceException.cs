// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.Events.V2.Exceptions
{
    public class EventV2ProcessingServiceException : Xeption
    {
        public EventV2ProcessingServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
