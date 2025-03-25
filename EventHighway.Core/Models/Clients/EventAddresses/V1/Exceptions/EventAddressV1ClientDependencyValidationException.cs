// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.EventAddresses.V1.Exceptions
{
    public class EventAddressV1ClientDependencyValidationException : Xeption
    {
        public EventAddressV1ClientDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
