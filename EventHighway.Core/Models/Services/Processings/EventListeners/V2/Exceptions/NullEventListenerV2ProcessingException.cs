// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventListeners.V2.Exceptions
{
    public class NullEventListenerV2ProcessingException : Xeption
    {
        public NullEventListenerV2ProcessingException(string message)
            : base(message)
        { }
    }
}
