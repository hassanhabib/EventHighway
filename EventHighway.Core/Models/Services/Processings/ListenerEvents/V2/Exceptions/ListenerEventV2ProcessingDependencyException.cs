// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2ProcessingDependencyException : Xeption
    {
        public ListenerEventV2ProcessingDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
