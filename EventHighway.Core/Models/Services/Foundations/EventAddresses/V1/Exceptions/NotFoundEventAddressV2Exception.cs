// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions
{
    public class NotFoundEventAddressV2Exception : Xeption
    {
        public NotFoundEventAddressV2Exception(string message)
            : base(message)
        { }
    }
}
