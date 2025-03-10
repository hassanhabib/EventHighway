// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2DependencyException : Xeption
    {
        public ListenerEventV2DependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
