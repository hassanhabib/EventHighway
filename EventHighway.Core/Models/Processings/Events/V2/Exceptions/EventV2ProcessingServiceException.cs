// ---------------------------------------------------------------
// Copyright (c) Aspen Publishing. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.Events.V2.Exceptions
{
    public class EventV2ProcessingServiceException : Xeption
    {
        public EventV2ProcessingServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
