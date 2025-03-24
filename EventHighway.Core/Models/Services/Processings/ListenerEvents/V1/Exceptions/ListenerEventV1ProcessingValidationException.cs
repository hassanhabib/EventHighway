// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.ListenerEvents.V1.Exceptions
{
    public class ListenerEventV1ProcessingValidationException : Xeption
    {
        public ListenerEventV1ProcessingValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
