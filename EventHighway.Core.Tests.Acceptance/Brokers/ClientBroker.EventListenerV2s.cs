// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker
    {
        public async ValueTask<EventListenerV2> RegisterEventListenerV2Async(EventListenerV2 eventListenerV2) =>
            await this.eventHighwayClient.EventListenerV2s.RegisterEventListenerV2Async(eventListenerV2);

        public async ValueTask<EventListenerV2> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id) =>
            await this.eventHighwayClient.EventListenerV2s.RemoveEventListenerV2ByIdAsync(eventListenerV2Id);
    }
}
