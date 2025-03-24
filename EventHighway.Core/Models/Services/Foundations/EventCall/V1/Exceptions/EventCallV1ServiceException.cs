// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions
{
    public class EventCallV1ServiceException : Xeption
    {
        public EventCallV1ServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
