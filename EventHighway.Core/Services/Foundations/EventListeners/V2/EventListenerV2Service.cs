// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal partial class EventListenerV2Service : IEventListenerV2Service
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventListenerV2Service(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventListenerV2> AddEventListenerV2Async(EventListenerV2 eventListenerV2) =>
        TryCatch(async () =>
        {
            await this.dateTimeBroker.GetDateTimeOffsetAsync();
            await ValidateEventListenerV2OnAddAsync(eventListenerV2);


            return await this.storageBroker.InsertEventListenerV2Async(eventListenerV2);
        });

        public ValueTask<IQueryable<EventListenerV2>> RetrieveAllEventListenerV2sAsync() =>
        TryCatch(async () => await storageBroker.SelectAllEventListenerV2sAsync());

        public ValueTask<EventListenerV2> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventListenerV2Id(eventListenerV2Id);

            EventListenerV2 maybeEventListenerV2 =
                await this.storageBroker.SelectEventListenerV2ByIdAsync(eventListenerV2Id);

            ValidateEventListenerV2Exists(maybeEventListenerV2, eventListenerV2Id);

            return await this.storageBroker.DeleteEventListenerV2Async(maybeEventListenerV2);
        });
    }
}
