// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class ListenerEventV1DependencyValidationException : Xeption
    {
        public ListenerEventV1DependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
