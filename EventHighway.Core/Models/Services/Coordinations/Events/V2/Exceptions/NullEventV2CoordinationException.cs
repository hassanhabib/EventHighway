// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions
{
    public class NullEventV2CoordinationException : Xeption
    {
        public NullEventV2CoordinationException(string message)
            : base(message)
        { }
    }
}
