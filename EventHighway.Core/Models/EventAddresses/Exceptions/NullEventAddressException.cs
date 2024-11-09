// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Xeptions;


namespace EventHighway.Core.Models.EventAddresses.Exceptions
{
    public class NullEventAddressException : Xeption
    {
        public NullEventAddressException(string message) : base(message:message) { }
    }
}