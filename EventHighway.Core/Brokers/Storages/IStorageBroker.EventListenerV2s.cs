// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<EventListenerV1> InsertEventListenerV2Async(EventListenerV1 eventListenerV2);
        ValueTask<IQueryable<EventListenerV1>> SelectAllEventListenerV2sAsync();
        ValueTask<EventListenerV1> SelectEventListenerV2ByIdAsync(Guid eventListenerV2Id);
        ValueTask<EventListenerV1> DeleteEventListenerV2Async(EventListenerV1 eventListenerV2);
    }
}
