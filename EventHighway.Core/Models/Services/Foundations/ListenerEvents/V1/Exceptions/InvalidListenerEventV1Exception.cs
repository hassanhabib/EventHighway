// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class InvalidListenerEventV1Exception : Xeption
    {
        public InvalidListenerEventV1Exception(string message)
            : base(message)
        { }
    }
}
