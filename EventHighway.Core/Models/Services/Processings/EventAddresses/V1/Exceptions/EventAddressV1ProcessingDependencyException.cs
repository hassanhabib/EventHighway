// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventAddresses.V1.Exceptions
{
    public class EventAddressV1ProcessingDependencyException : Xeption
    {
        public EventAddressV1ProcessingDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
