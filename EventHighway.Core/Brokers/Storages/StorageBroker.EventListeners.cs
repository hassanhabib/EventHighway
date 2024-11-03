// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<EventListener> EventListeners { get; set; }

        public async ValueTask<EventListener> InsertEventListenerAsync(EventListener eventListener) =>
            await this.InsertAsync(eventListener);
    }
}
