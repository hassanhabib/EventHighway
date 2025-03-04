// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<EventListener> EventListeners { get; set; }

        public async ValueTask<EventListener> InsertEventListenerAsync(EventListener eventListener) =>
            await InsertAsync(eventListener);

        public async ValueTask<IQueryable<EventListener>> SelectAllEventListenersAsync() =>
            SelectAll<EventListener>();
    }
}
