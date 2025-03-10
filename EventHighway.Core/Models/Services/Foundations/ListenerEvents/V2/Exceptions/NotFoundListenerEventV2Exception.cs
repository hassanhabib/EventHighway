// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2.Exceptions
{
    public class NotFoundListenerEventV2Exception : Xeption
    {
        public NotFoundListenerEventV2Exception(string message)
            : base(message)
        { }
    }
}
