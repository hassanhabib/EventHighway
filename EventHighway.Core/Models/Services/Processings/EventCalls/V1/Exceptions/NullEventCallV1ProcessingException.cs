// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.EventCalls.V1.Exceptions
{
    public class NullEventCallV1ProcessingException : Xeption
    {
        public NullEventCallV1ProcessingException(string message)
            : base(message)
        { }
    }
}
