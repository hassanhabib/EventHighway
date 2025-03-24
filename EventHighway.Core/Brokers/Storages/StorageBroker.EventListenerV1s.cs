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

        public async ValueTask<EventListenerV1> InsertEventListenerV1Async(EventListenerV1 eventListenerV1) =>
            await InsertAsync(eventListenerV1);

        public async ValueTask<IQueryable<EventListenerV1>> SelectAllEventListenerV1sAsync() =>
            SelectAll<EventListenerV1>();

        public async ValueTask<EventListenerV1> SelectEventListenerV1ByIdAsync(Guid eventListenerV1Id) =>
            await SelectAsync<EventListenerV1>(eventListenerV1Id);

        public async ValueTask<EventListenerV1> DeleteEventListenerV1Async(EventListenerV1 eventListenerV1) =>
            await DeleteAsync(eventListenerV1);
    }
}
