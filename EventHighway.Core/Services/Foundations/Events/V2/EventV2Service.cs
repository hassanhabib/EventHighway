// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.Events.V2;

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

        public ValueTask<EventV2> AddEventV2Async(EventV2 eventV2) =>
        TryCatch(async () =>
        {
            await ValidateEventV2OnAddAsync(eventV2);

            return await storageBroker.InsertEventV2Async(eventV2);
        });

        public ValueTask<IQueryable<EventV2>> RetrieveAllEventV2sAsync() =>
        TryCatch(async () => await this.storageBroker.SelectAllEventV2sAsync());

        public ValueTask<EventV2> RemoveEventV2ByIdAsync(Guid eventV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventV2Id(eventV2Id);

            EventV2 maybeEventV2 =
                await this.storageBroker.SelectEventV2ByIdAsync(eventV2Id);

            return await this.storageBroker.DeleteEventV2Async(maybeEventV2);
        });
    }
}
