// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.Events.V2;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial class EventV2Service : IEventV2Service
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventV2Service(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventV2> AddEventV2Async(EventV2 eventV2) =>
        TryCatch(async () =>
        {
            await ValidateEventV2OnAddAsync(eventV2);

            return await storageBroker.InsertEventV2Async(eventV2);
        });
    }
}
