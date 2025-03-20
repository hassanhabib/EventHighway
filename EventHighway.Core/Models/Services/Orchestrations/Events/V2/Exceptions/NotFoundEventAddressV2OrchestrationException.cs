// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions
{
    public class NotFoundEventAddressV2OrchestrationException : Xeption
    {
        public NotFoundEventAddressV2OrchestrationException(string message)
            : base(message)
        { }
    }
}
