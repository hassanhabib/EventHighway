// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        public DbSet<EventAddress> EventAddresses { get; set; }

        public async ValueTask<EventAddress> InsertEventAddressAsync(EventAddress eventAddress) =>
            await InsertAsync(eventAddress);

        public async ValueTask<IQueryable<EventAddress>> SelectAllEventAddressesAsync() =>
            SelectAll<EventAddress>();

        public async ValueTask<EventAddress> SelectEventAddressByIdAsync(Guid eventAddressId) =>
            await SelectAsync<EventAddress>(eventAddressId);
    }
}
