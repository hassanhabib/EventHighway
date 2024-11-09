// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.EventAddresses.Exceptions
{
    public class EventAddressValidationException : Xeption
    {
        public EventAddressValidationException(string message, Xeption innerException)
            : base(message: message, innerException: innerException) { }
    }
}