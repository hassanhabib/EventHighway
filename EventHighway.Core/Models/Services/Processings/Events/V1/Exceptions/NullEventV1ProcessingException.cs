// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.Events.V1.Exceptions
{
    public class NullEventV1ProcessingException : Xeption
    {
        public NullEventV1ProcessingException(string message)
            : base(message)
        { }
    }
}
