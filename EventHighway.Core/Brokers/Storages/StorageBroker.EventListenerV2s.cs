// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<EventListenerV1> EventListenerV1s { get; set; }

        public async ValueTask<EventListenerV1> InsertEventListenerV2Async(EventListenerV1 eventListenerV2) =>
            await InsertAsync(eventListenerV2);

        public async ValueTask<IQueryable<EventListenerV1>> SelectAllEventListenerV2sAsync() =>
            SelectAll<EventListenerV1>();

        public async ValueTask<EventListenerV1> SelectEventListenerV2ByIdAsync(Guid eventListenerV2Id) =>
            await SelectAsync<EventListenerV1>(eventListenerV2Id);

        public async ValueTask<EventListenerV1> DeleteEventListenerV2Async(EventListenerV1 eventListenerV2) =>
            await DeleteAsync(eventListenerV2);
    }
}
