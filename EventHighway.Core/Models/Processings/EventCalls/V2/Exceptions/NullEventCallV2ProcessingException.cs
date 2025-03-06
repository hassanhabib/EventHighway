// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
