// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.ListenerEvents;

namespace EventHighway.Core.Services.ListernEvents
{
    internal class ListenerEventService : IListenerEventService
    {
        private readonly IStorageBroker storageBroker;

        public ListenerEventService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<ListenerEvent> AddListenerEventAsync(ListenerEvent listenerEvent) =>
            await this.storageBroker.InsertListenerEventAsync(listenerEvent);
    }
}
