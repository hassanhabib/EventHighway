// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.EventAddresses;

namespace EventHighway.Core.Services.EventAddresses
{
    internal partial class EventAddressService : IEventAddressService
    {
        private readonly IStorageBroker storageBroker;

        public EventAddressService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<EventAddress> AddEventAddressAsync(EventAddress eventAddress) =>
            await this.storageBroker.InsertEventAddressAsync(eventAddress);
    }
}
