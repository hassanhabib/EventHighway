﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.Services.Foundations.Events;

namespace EventHighway.Core.Services.Foundations.Events
{
    internal partial class EventService : IEventService
    {
        private readonly IStorageBroker storageBroker;

        public EventService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Event> AddEventAsync(Event @event) =>
            await storageBroker.InsertEventAsync(@event);
    }
}
