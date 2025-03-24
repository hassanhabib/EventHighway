// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<EventV1> InsertEventV2Async(EventV1 eventV2);
        ValueTask<IQueryable<EventV1>> SelectAllEventV2sAsync();
        ValueTask<EventV1> SelectEventV2ByIdAsync(Guid eventV2Id);
        ValueTask<EventV1> DeleteEventV2Async(EventV1 eventV2);
    }
}
