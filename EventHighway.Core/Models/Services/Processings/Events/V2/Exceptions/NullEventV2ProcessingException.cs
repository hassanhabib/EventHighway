// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Processings.Events.V2.Exceptions
{
    public class NullEventV2ProcessingException : Xeption
    {
        public NullEventV2ProcessingException(string message)
            : base(message)
        { }
    }
}
