// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.EventAddresses;

namespace EventHighway.Core.Services.Foundations.EventAddresses
{
    internal partial class EventAddressService : IEventAddressService
    {
        private readonly IStorageBroker storageBroker;

        public EventAddressService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<EventAddress> AddEventAddressAsync(EventAddress eventAddress) =>
            await storageBroker.InsertEventAddressAsync(eventAddress);

        public async ValueTask<IQueryable<EventAddress>> RetrieveAllEventAddressesAsync() =>
            await this.storageBroker.SelectAllEventAddressesAsync();

        public async ValueTask<EventAddress> RetrieveEventAddressByIdAsync(Guid eventAddressId) =>
            await storageBroker.SelectEventAddressByIdAsync(eventAddressId);
    }
}
