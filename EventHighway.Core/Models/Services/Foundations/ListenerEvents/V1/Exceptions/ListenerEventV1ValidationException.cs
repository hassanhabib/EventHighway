// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class ListenerEventV1ValidationException : Xeption
    {
        public ListenerEventV1ValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
