// ---------------------------------------------------------------
// Copyright (c) Aspen Publishing. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.EventCalls.V2.Exceptions
{
    public class EventCallV2ProcessingDependencyValidationException : Xeption
    {
        public EventCallV2ProcessingDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
