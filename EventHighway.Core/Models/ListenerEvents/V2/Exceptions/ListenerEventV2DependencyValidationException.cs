// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2DependencyValidationException : Xeption
    {
        public ListenerEventV2DependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
