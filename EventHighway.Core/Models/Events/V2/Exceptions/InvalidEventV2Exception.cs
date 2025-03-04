// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Events.V2.Exceptions
{
    public class InvalidEventV2Exception : Xeption
    {
        public InvalidEventV2Exception(string message)
            : base(message)
        { }
    }
}
