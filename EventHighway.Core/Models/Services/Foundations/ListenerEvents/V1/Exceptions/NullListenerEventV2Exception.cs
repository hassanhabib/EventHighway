// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class NullListenerEventV2Exception : Xeption
    {
        public NullListenerEventV2Exception(string message)
            : base(message)
        { }
    }
}
