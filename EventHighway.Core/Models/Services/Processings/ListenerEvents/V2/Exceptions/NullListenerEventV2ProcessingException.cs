// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions
{
    public class NullListenerEventV2ProcessingException : Xeption
    {
        public NullListenerEventV2ProcessingException(string message)
            : base(message)
        { }
    }
}
