// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V2.Exceptions
{
    public class NullEventCallV2Exception : Xeption
    {
        public NullEventCallV2Exception(string message)
            : base(message)
        { }
    }
}
