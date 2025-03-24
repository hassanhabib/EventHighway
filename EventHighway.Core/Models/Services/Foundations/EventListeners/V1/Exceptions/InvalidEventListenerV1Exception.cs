// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions
{
    public class InvalidEventListenerV1Exception : Xeption
    {
        public InvalidEventListenerV1Exception(string message)
            : base(message)
        { }
    }
}
