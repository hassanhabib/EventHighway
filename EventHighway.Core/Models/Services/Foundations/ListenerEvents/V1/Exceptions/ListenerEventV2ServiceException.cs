// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions
{
    public class ListenerEventV2ServiceException : Xeption
    {
        public ListenerEventV2ServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
