// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions
{
    public class EventCallV1DependencyValidationException : Xeption
    {
        public EventCallV1DependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
