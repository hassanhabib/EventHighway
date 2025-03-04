// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.ListenerEvents.V2.Exceptions
{
    public class NullListenerEventV2Exception : Xeption
    {
        public NullListenerEventV2Exception(string message)
            : base(message)
        { }
    }
}
