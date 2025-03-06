// ---------------------------------------------------------------
// Copyright (c) Aspen Publishing. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Processings.EventCalls.V2.Exceptions
{
    public class NullEventCallV2ProcessingException : Xeption
    {
        public NullEventCallV2ProcessingException(string message)
            : base(message)
        { }
    }
}
