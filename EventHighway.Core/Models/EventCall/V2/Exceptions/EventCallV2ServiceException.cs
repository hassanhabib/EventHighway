// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.EventCall.V2.Exceptions
{
    public class EventCallV2ServiceException : Xeption
    {
        public EventCallV2ServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
