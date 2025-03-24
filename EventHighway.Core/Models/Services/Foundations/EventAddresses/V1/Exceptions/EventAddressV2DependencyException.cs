// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions
{
    public class EventAddressV2DependencyException : Xeption
    {
        public EventAddressV2DependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
