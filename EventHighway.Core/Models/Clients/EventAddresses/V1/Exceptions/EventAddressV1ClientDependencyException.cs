// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Clients.EventAddresses.V1.Exceptions
{
    public class EventAddressV1ClientDependencyException : Xeption
    {
        public EventAddressV1ClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
