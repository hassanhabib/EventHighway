// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.Events.V1.Exceptions
{
    public class EventV2ServiceException : Xeption
    {
        public EventV2ServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
