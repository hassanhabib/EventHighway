// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents.V2;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<ListenerEventV2> ListenerEventV2s { get; set; }

        public async ValueTask<ListenerEventV2> InsertListenerEventV2Async(ListenerEventV2 listenerEventV2) =>
            await InsertAsync(listenerEventV2);

        public async ValueTask<ListenerEventV2> UpdateListenerEventV2Async(ListenerEventV2 listenerEventV2) =>
            await UpdateAsync(listenerEventV2);
    }
}
