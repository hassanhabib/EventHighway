// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Services.Orchestrations.EventListeners.V2;

namespace EventHighway.Core.Clients.ListenerEvents.V2
{
    internal class ListenerEventV2sClient : IListenerEventV2sClient
    {
        private readonly IEventListenerV2OrchestrationService eventListenerV2OrchestrationService;

        public ListenerEventV2sClient(IEventListenerV2OrchestrationService eventListenerV2OrchestrationService) =>
            this.eventListenerV2OrchestrationService = eventListenerV2OrchestrationService;

        public async ValueTask<IQueryable<ListenerEventV2>> RetrieveAllListenerEventV2sAsync()
        {
            return await this.eventListenerV2OrchestrationService
                .RetrieveAllListenerEventV2sAsync();
        }
    }
}
