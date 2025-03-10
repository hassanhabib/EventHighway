// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Coordinations.Events.V2.Exceptions
{
    public class NullEventCallV2CoordinationException : Xeption
    {
        public NullEventCallV2CoordinationException(string message)
            : base(message)
        { }
    }
}
