// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.Events.V1;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial class EventV2Service : IEventV2Service
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventV2Service(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventV1> AddEventV2Async(EventV1 eventV2) =>
        TryCatch(async () =>
        {
            await ValidateEventV2OnAddAsync(eventV2);

            return await storageBroker.InsertEventV1Async(eventV2);
        });

        public ValueTask<IQueryable<EventV1>> RetrieveAllEventV2sAsync() =>
        TryCatch(async () => await this.storageBroker.SelectAllEventV1sAsync());

        public ValueTask<EventV1> RemoveEventV2ByIdAsync(Guid eventV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventV2Id(eventV2Id);

            EventV1 maybeEventV2 =
                await this.storageBroker.SelectEventV1ByIdAsync(eventV2Id);

            ValidateEventV2Exists(maybeEventV2, eventV2Id);

            return await this.storageBroker.DeleteEventV1Async(maybeEventV2);
        });
    }
}
