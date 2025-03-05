// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.EventCall.V2.Exceptions
{
    public class EventCallV2DependencyValidationException : Xeption
    {
        public EventCallV2DependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
