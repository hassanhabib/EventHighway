// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2ProcessingServiceException : Xeption
    {
        public ListenerEventV2ProcessingServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
