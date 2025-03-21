// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker
    {
        public async ValueTask<IQueryable<ListenerEventV2>> RetrieveAllListenerEventV2s() =>
            await this.eventHighwayClient.ListenerEventV2s.RetrieveAllListenerEventV2sAsync();

        public async ValueTask<ListenerEventV2> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id) =>
            await this.eventHighwayClient.ListenerEventV2s.RemoveListenerEventV2ByIdAsync(listenerEventV2Id);
    }
}
