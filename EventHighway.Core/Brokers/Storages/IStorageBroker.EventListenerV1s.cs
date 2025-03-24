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
        ValueTask<EventListenerV1> InsertEventListenerV1Async(EventListenerV1 eventListenerV1);
        ValueTask<IQueryable<EventListenerV1>> SelectAllEventListenerV1sAsync();
        ValueTask<EventListenerV1> SelectEventListenerV1ByIdAsync(Guid eventListenerV1Id);
        ValueTask<EventListenerV1> DeleteEventListenerV1Async(EventListenerV1 eventListenerV1);
    }
}
