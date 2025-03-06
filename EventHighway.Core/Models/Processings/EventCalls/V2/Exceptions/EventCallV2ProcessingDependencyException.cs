// ---------------------------------------------------------------
// Copyright (c) Aspen Publishing. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.EventCalls.V2.Exceptions
{
    public class EventCallV2ProcessingDependencyException : Xeption
    {
        public EventCallV2ProcessingDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
