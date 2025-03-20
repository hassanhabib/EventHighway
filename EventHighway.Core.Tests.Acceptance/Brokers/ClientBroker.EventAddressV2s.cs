// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using System.Threading.Tasks;
using System;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker
    {
        public async ValueTask<EventAddressV2> RegisterEventAddressV2Async(EventAddressV2 eventListenerV2) =>
            await this.eventHighwayClient.IEventAddressV2s.RegisterEventAddressV2Async(eventListenerV2);

        public async ValueTask<EventAddressV2> RemoveEventAddressV2ByIdAsync(Guid eventListenerV2Id) =>
            await this.eventHighwayClient.IEventAddressV2s.RemoveEventAddressV2ByIdAsync(eventListenerV2Id);
    }
}
