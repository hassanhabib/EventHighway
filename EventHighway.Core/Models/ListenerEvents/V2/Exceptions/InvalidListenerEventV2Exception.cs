// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.ListenerEvents.V2.Exceptions
{
    public class InvalidListenerEventV2Exception : Xeption
    {
        public InvalidListenerEventV2Exception(string message)
            : base(message)
        { }
    }
}
