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
using EventHighway.Core.Services.Processings.ListenerEvents.V1;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V1
{
    internal partial class EventListenerV1OrchestrationService : IEventListenerV1OrchestrationService
    {
        private readonly IEventListenerV1ProcessingService eventListenerV1ProcessingService;
        private readonly IListenerEventV1ProcessingService listenerEventV1ProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public EventListenerV1OrchestrationService(
            IEventListenerV1ProcessingService eventListenerV1ProcessingService,
            IListenerEventV1ProcessingService listenerEventV1ProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.eventListenerV1ProcessingService = eventListenerV1ProcessingService;
            this.listenerEventV1ProcessingService = listenerEventV1ProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventListenerV1> AddEventListenerV1Async(EventListenerV1 eventListenerV1) =>
        TryCatch(async () =>
        {
            ValidateEventListenerV1IsNotNull(eventListenerV1);

            return await this.eventListenerV1ProcessingService.AddEventListenerV1Async(
                eventListenerV1);
        });

        public ValueTask<IQueryable<EventListenerV1>> RetrieveEventListenerV1sByEventAddressIdAsync(
            Guid eventAddressId) => TryCatch(async () =>
        {
            ValidateEventAddressId(eventAddressId);

            return await this.eventListenerV1ProcessingService
                .RetrieveEventListenerV1sByEventAddressIdAsync(eventAddressId);
        });

        public ValueTask<EventListenerV1> RemoveEventListenerV1ByIdAsync(Guid eventListenerV1Id) =>
        TryCatch(async () =>
        {
            ValidateEventListenerV1Id(eventListenerV1Id);

            return await this.eventListenerV1ProcessingService.RemoveEventListenerV1ByIdAsync(
                eventListenerV1Id);
        });

        public ValueTask<ListenerEventV1> AddListenerEventV1Async(ListenerEventV1 listenerEventV1) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV1IsNotNull(listenerEventV1);

            return await this.listenerEventV1ProcessingService.AddListenerEventV1Async(
                listenerEventV1);
        });

        public ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV1sAsync() =>
        TryCatch(async () =>
        {
            return await this.listenerEventV1ProcessingService
                .RetrieveAllListenerEventV1sAsync();
        });

        public ValueTask<ListenerEventV1> ModifyListenerEventV1Async(ListenerEventV1 listenerEventV1) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV1IsNotNull(listenerEventV1);

            return await this.listenerEventV1ProcessingService.ModifyListenerEventV1Async(
                listenerEventV1);
        });

        public ValueTask<ListenerEventV1> RemoveListenerEventV1ByIdAsync(Guid listenerEventV1Id) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV1Id(listenerEventV1Id);

            return await this.listenerEventV1ProcessingService
                .RemoveListenerEventV1ByIdAsync(listenerEventV1Id);
        });
    }
}
