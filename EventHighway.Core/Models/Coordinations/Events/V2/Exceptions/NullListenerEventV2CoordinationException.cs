// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Coordinations.Events.V2.Exceptions
{
    public class NullListenerEventV2CoordinationException : Xeption
    {
        public NullListenerEventV2CoordinationException(string message)
            : base(message)
        { }
    }
}
