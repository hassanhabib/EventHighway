// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Services.Foundations.EventListeners.V1;

namespace EventHighway.Core.Services.Processings.EventListeners.V2
{
    internal partial class EventListenerV2ProcessingService : IEventListenerV2ProcessingService
    {
        private readonly IEventListenerV1Service eventListenerV2Service;
        private readonly ILoggingBroker loggingBroker;

        public EventListenerV2ProcessingService(
            IEventListenerV1Service eventListenerV2Service,
            ILoggingBroker loggingBroker)
        {
            this.eventListenerV2Service = eventListenerV2Service;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventListenerV1> AddEventListenerV2Async(EventListenerV1 eventListenerV2) =>
        TryCatch(async () =>
        {
            ValidateEventListenerV2IsNotNull(eventListenerV2);

            return await this.eventListenerV2Service.AddEventListenerV1Async(eventListenerV2);
        });

        public ValueTask<IQueryable<EventListenerV1>> RetrieveEventListenerV2sByEventAddressIdAsync(
            Guid eventAddressId) => TryCatch(async () =>
        {
            ValidateEventAddressId(eventAddressId);

            IQueryable<EventListenerV1> eventListenerV2s =
                await this.eventListenerV2Service.RetrieveAllEventListenerV1sAsync();

            return eventListenerV2s.Where(eventListenerV2 =>
                eventListenerV2.EventAddressId == eventAddressId);
        });

        public ValueTask<EventListenerV1> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventListenerV2Id(eventListenerV2Id);

            return await this.eventListenerV2Service.RemoveEventListenerV1ByIdAsync(
                eventListenerV2Id);
        });
    }
}
