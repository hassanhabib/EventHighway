// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2ProcessingValidationException : Xeption
    {
        public ListenerEventV2ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
