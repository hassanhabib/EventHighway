// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners.V2;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<EventListenerV2> EventListenerV2s { get; set; }

        public async ValueTask<EventListenerV2> InsertEventListenerV2Async(EventListenerV2 eventListenerV2) =>
            await InsertAsync(eventListenerV2);

        public async ValueTask<IQueryable<EventListenerV2>> SelectAllEventListenerV2sAsync() =>
            SelectAll<EventListenerV2>();
    }
}
