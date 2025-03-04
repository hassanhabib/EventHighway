// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Events.V2;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<EventV2> EventV2s { get; set; }

        public async ValueTask<EventV2> InsertEventV2Async(EventV2 eventV2) =>
            await this.InsertAsync(eventV2);

        public async ValueTask<IQueryable<EventV2>> SelectAllEventV2sAsync() =>
            SelectAll<EventV2>();
    }
}
