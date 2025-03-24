// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<EventV1> EventV2s { get; set; }

        public async ValueTask<EventV1> InsertEventV2Async(EventV1 eventV2) =>
            await InsertAsync(eventV2);

        public async ValueTask<IQueryable<EventV1>> SelectAllEventV2sAsync() =>
            SelectAll<EventV1>();

        public async ValueTask<EventV1> SelectEventV2ByIdAsync(Guid eventV2Id) =>
            await SelectAsync<EventV1>(eventV2Id);

        public async ValueTask<EventV1> DeleteEventV2Async(EventV1 eventV2) =>
            await DeleteAsync(eventV2);
    }
}
