// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.EventListeners;

namespace EventHighway.Core.Services.EventListeners
{
    internal class EventListenerService : IEventListenerService
    {
        private readonly IStorageBroker storageBroker;

        public EventListenerService(IStorageBroker storageBroker) => 
            this.storageBroker = storageBroker;

        public async ValueTask<EventListener> AddEventListenerAsync(EventListener eventListener) =>
            await this.storageBroker.InsertEventListenerAsync(eventListener);
    }
}
