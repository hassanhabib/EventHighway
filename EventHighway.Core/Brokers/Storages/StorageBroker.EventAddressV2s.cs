// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<EventAddressV1> EventAddressV1s { get; set; }

        public async ValueTask<EventAddressV1> InsertEventAddressV2Async(EventAddressV1 eventAddressV2) =>
            await InsertAsync(eventAddressV2);

        public async ValueTask<EventAddressV1> SelectEventAddressV2ByIdAsync(Guid eventAddressV2Id) =>
            await SelectAsync<EventAddressV1>(eventAddressV2Id);

        public async ValueTask<EventAddressV1> DeleteEventAddressV2Async(EventAddressV1 eventAddressV2) =>
            await DeleteAsync(eventAddressV2);
    }
}
