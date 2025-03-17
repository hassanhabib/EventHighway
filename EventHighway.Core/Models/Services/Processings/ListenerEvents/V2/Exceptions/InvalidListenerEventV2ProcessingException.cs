// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions
{
    public class InvalidListenerEventV2ProcessingException : Xeption
    {
        public InvalidListenerEventV2ProcessingException(string message)
            : base(message)
        { }
    }
}
