// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.EventAddresses;

namespace EventHighway.Core.Services.Foundations.EventAddresses
{
    internal partial class EventAddressService : IEventAddressService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventAddressService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<EventAddress> AddEventAddressAsync(EventAddress eventAddress) =>
            await TryCatch(async () =>
            {
                ValidateEventAddressIsNotNull(eventAddress);

                return await storageBroker.InsertEventAddressAsync(eventAddress);
            });

        public async ValueTask<EventAddress> RetrieveEventAddressByIdAsync(Guid eventAddressId) =>
            await TryCatch(async () =>
            {
                return await storageBroker.SelectEventAddressByIdAsync(eventAddressId);
            });
    }
}