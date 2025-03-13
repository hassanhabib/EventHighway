// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions
{
    public class EventAddressV2ValidationException : Xeption
    {
        public EventAddressV2ValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
