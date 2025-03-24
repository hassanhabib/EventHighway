// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;

namespace EventHighway.Core.Services.Coordinations.Events.V2
{
    internal interface IEventV2CoordinationService
    {
        ValueTask<EventV1> SubmitEventV2Async(EventV1 eventV2);
        ValueTask FireScheduledPendingEventV2sAsync();
        ValueTask<EventV1> RemoveEventV2ByIdAsync(Guid eventV2Id);
    }
}
