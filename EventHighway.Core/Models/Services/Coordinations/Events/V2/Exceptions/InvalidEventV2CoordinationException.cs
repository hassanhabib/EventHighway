// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions
{
    public class InvalidEventV2CoordinationException : Xeption
    {
        public InvalidEventV2CoordinationException(string message)
            : base(message)
        { }
    }
}
