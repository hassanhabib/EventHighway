// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Services.Processings.EventListeners.V1;
using EventHighway.Core.Services.Processings.ListenerEvents.V2;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal partial class EventListenerV2OrchestrationService : IEventListenerV2OrchestrationService
    {
        private readonly IEventListenerV1ProcessingService eventListenerV2ProcessingService;
        private readonly IListenerEventV2ProcessingService listenerEventV2ProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public EventListenerV2OrchestrationService(
            IEventListenerV1ProcessingService eventListenerV2ProcessingService,
            IListenerEventV2ProcessingService listenerEventV2ProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.eventListenerV2ProcessingService = eventListenerV2ProcessingService;
            this.listenerEventV2ProcessingService = listenerEventV2ProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventListenerV1> AddEventListenerV2Async(EventListenerV1 eventListenerV2) =>
        TryCatch(async () =>
        {
            ValidateEventListenerV2IsNotNull(eventListenerV2);

            return await this.eventListenerV2ProcessingService.AddEventListenerV1Async(
                eventListenerV2);
        });

        public ValueTask<IQueryable<EventListenerV1>> RetrieveEventListenerV2sByEventAddressIdAsync(
            Guid eventAddressId) => TryCatch(async () =>
        {
            ValidateEventAddressId(eventAddressId);

            return await this.eventListenerV2ProcessingService
                .RetrieveEventListenerV1sByEventAddressIdAsync(eventAddressId);
        });

        public ValueTask<EventListenerV1> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventListenerV2Id(eventListenerV2Id);

            return await this.eventListenerV2ProcessingService.RemoveEventListenerV1ByIdAsync(
                eventListenerV2Id);
        });

        public ValueTask<ListenerEventV1> AddListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV2IsNotNull(listenerEventV2);

            return await this.listenerEventV2ProcessingService.AddListenerEventV2Async(
                listenerEventV2);
        });

        public ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV2sAsync() =>
        TryCatch(async () =>
        {
            return await this.listenerEventV2ProcessingService
                .RetrieveAllListenerEventV2sAsync();
        });

        public ValueTask<ListenerEventV1> ModifyListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV2IsNotNull(listenerEventV2);

            return await this.listenerEventV2ProcessingService.ModifyListenerEventV2Async(
                listenerEventV2);
        });

        public ValueTask<ListenerEventV1> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV2Id(listenerEventV2Id);

            return await this.listenerEventV2ProcessingService
                .RemoveListenerEventV2ByIdAsync(listenerEventV2Id);
        });
    }
}
