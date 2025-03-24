// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Foundations.Events.V1.Exceptions
{
    public class NotFoundEventV2Exception : Xeption
    {
        public NotFoundEventV2Exception(string message)
            : base(message)
        { }
    }
}
