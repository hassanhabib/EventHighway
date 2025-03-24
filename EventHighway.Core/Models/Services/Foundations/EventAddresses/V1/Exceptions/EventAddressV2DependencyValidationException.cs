// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions
{
    public class EventAddressV2DependencyValidationException : Xeption
    {
        public EventAddressV2DependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
