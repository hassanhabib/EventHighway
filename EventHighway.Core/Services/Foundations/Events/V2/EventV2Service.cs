// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.Events.V2;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial class EventV2Service : IEventV2Service
    {
        private readonly IStorageBroker storageBroker;

        public EventV2Service(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<EventV2> AddEventV2Async(EventV2 eventV2) =>
            await storageBroker.InsertEventV2Async(eventV2);
    }
}
