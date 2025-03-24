// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions
{
    public class InvalidEventAddressV1Exception : Xeption
    {
        public InvalidEventAddressV1Exception(string message)
            : base(message)
        { }
    }
}
