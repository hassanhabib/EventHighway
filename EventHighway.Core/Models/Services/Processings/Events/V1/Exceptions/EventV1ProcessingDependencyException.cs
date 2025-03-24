// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.Events.V1.Exceptions
{
    public class EventV1ProcessingDependencyException : Xeption
    {
        public EventV1ProcessingDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
