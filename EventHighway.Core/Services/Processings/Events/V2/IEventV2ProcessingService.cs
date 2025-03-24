// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;

namespace EventHighway.Core.Services.Processings.Events.V2
{
    internal interface IEventV2ProcessingService
    {
        ValueTask<EventV1> AddEventV2Async(EventV1 eventV2);
        ValueTask<IQueryable<EventV1>> RetrieveScheduledPendingEventV2sAsync();
        ValueTask<EventV1> RemoveEventV2ByIdAsync(Guid eventV2Id);
    }
}
