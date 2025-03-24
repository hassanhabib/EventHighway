// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class InvalidListenerEventV2Exception : Xeption
    {
        public InvalidListenerEventV2Exception(string message)
            : base(message)
        { }
    }
}
