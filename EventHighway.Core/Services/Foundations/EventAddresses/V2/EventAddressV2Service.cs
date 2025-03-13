// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;

namespace EventHighway.Core.Services.Foundations.EventAddresses.V2
{
    internal partial class EventAddressV2Service : IEventAddressV2Service
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventAddressV2Service(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventAddressV2> AddEventAddressV2Async(EventAddressV2 eventAddressV2) =>
        TryCatch(async () =>
        {
            ValidateEventAddressV2IsNotNull(eventAddressV2);
            await this.dateTimeBroker.GetDateTimeOffsetAsync();

            return await this.storageBroker.InsertEventAddressV2Async(eventAddressV2);
        });

        public ValueTask<EventAddressV2> RetrieveEventAddressV2ByIdAsync(Guid eventAddressV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventAddressV2Id(eventAddressV2Id);

            return await this.storageBroker.SelectEventAddressV2ByIdAsync(eventAddressV2Id);
        });
    }
}
