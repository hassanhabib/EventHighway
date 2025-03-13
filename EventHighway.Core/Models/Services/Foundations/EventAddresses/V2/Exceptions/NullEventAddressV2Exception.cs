// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions
{
    public class NullEventAddressV2Exception : Xeption
    {
        public NullEventAddressV2Exception(string message)
            : base(message)
        { }
    }
}
