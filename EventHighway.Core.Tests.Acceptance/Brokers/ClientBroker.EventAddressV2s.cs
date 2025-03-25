// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker
    {
        public async ValueTask<EventAddressV1> RegisterEventAddressV1Async(EventAddressV1 eventAddressV1) =>
            await this.eventHighwayClient.IEventAddressV1s.RegisterEventAddressV1Async(eventAddressV1);

        public async ValueTask<EventAddressV1> RemoveEventAddressV1ByIdAsync(Guid eventAddressV1Id) =>
            await this.eventHighwayClient.IEventAddressV1s.RemoveEventAddressV1ByIdAsync(eventAddressV1Id);
    }
}
