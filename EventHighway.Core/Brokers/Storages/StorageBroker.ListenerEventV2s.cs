// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<ListenerEventV1> ListenerEventV1s { get; set; }

        public async ValueTask<ListenerEventV1> InsertListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
            await InsertAsync(listenerEventV2);

        public async ValueTask<IQueryable<ListenerEventV1>> SelectAllListenerEventV2sAsync() =>
            SelectAll<ListenerEventV1>();

        public async ValueTask<ListenerEventV1> SelectListenerEventV2ByIdAsync(Guid listenerEventV2Id) =>
            await SelectAsync<ListenerEventV1>(listenerEventV2Id);

        public async ValueTask<ListenerEventV1> UpdateListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
            await UpdateAsync(listenerEventV2);

        public async ValueTask<ListenerEventV1> DeleteListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
            await DeleteAsync(listenerEventV2);
    }
}
