// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker
    {
        public async ValueTask<EventAddressV2> RegisterEventAddressV2Async(EventAddressV2 eventAddressV2) =>
            await this.eventHighwayClient.IEventAddressV2s.RegisterEventAddressV2Async(eventAddressV2);

        public async ValueTask<EventAddressV2> RemoveEventAddressV2ByIdAsync(Guid eventAddressV2Id) =>
            await this.eventHighwayClient.IEventAddressV2s.RemoveEventAddressV2ByIdAsync(eventAddressV2Id);
    }
}
