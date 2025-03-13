// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal partial class EventListenerV2Service : IEventListenerV2Service
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventListenerV2Service(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IQueryable<EventListenerV2>> RetrieveAllEventListenerV2sAsync() =>
        TryCatch(async () => await storageBroker.SelectAllEventListenerV2sAsync());

        public ValueTask<EventListenerV2> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventListenerV2Id(eventListenerV2Id);

            EventListenerV2 maybeEventListenerV2 =
                await this.storageBroker.SelectEventListenerV2ByIdAsync(eventListenerV2Id);

            return await this.storageBroker.DeleteEventListenerV2Async(maybeEventListenerV2);
        });
    }
}
