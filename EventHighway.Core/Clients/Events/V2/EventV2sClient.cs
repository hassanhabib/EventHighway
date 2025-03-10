// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Services.Coordinations.Events.V2;

namespace EventHighway.Core.Clients.Events.V2
{
    internal class EventV2sClient : IEventV2sClient
    {
        private readonly IEventV2CoordinationService eventV2CoordinationService;

        public EventV2sClient(IEventV2CoordinationService eventV2CoordinationService) =>
            this.eventV2CoordinationService = eventV2CoordinationService;

        public async ValueTask FireScheduledPendingEventV2sAsync()
        {
            await this.eventV2CoordinationService
                .FireScheduledPendingEventV2sAsync();
        }
    }
}
