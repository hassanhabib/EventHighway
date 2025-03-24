// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.ListenerEvents.V1.Exceptions
{
    public class InvalidListenerEventV1ProcessingException : Xeption
    {
        public InvalidListenerEventV1ProcessingException(string message)
            : base(message)
        { }
    }
}
