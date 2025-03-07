// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Services.Processings.EventListeners.V2;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal partial class EventListenerV2OrchestrationService : IEventListenerV2OrchestrationService
    {
        private readonly IEventListenerV2ProcessingService eventListenerV2ProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public EventListenerV2OrchestrationService(
            IEventListenerV2ProcessingService eventListenerV2ProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.eventListenerV2ProcessingService = eventListenerV2ProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<IQueryable<EventListenerV2>> RetrieveEventListenerV2sByEventAddressIdAsync(
            Guid eventAddressId)
        {
            return await this.eventListenerV2ProcessingService.RetrieveEventListenerV2sByEventAddressIdAsync(
                eventAddressId);
        }
    }
}
