// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Services.Processings.EventListeners.V2;
using EventHighway.Core.Services.Processings.ListenerEvents.V2;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal partial class EventListenerV2OrchestrationService : IEventListenerV2OrchestrationService
    {
        private readonly IEventListenerV2ProcessingService eventListenerV2ProcessingService;
        private readonly IListenerEventV2ProcessingService listenerEventV2ProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public EventListenerV2OrchestrationService(
            IEventListenerV2ProcessingService eventListenerV2ProcessingService,
            IListenerEventV2ProcessingService listenerEventV2ProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.eventListenerV2ProcessingService = eventListenerV2ProcessingService;
            this.listenerEventV2ProcessingService = listenerEventV2ProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IQueryable<EventListenerV2>> RetrieveEventListenerV2sByEventAddressIdAsync(
            Guid eventAddressId) => TryCatch(async () =>
        {
            ValidateEventAddressId(eventAddressId);

            return await this.eventListenerV2ProcessingService
                .RetrieveEventListenerV2sByEventAddressIdAsync(eventAddressId);
        });

        public ValueTask<ListenerEventV2> AddListenerEventV2Async(ListenerEventV2 listenerEventV2) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV2IsNotNull(listenerEventV2);

            return await this.listenerEventV2ProcessingService.AddListenerEventV2Async(
                listenerEventV2);
        });
    }
}
