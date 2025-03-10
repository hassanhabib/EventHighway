// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.Events.V2.Exceptions
{
    public class NullEventV2Exception : Xeption
    {
        public NullEventV2Exception(string message)
            : base(message)
        { }
    }
}
