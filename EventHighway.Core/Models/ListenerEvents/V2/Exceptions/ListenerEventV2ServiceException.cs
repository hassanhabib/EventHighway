// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.ListenerEvents.V2.Exceptions
{
    public class ListenerEventV2ServiceException : Xeption
    {
        public ListenerEventV2ServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
