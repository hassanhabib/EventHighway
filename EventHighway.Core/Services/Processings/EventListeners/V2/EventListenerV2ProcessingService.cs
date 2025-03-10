// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Services.Foundations.EventListeners.V2;

namespace EventHighway.Core.Services.Processings.EventListeners.V2
{
    internal partial class EventListenerV2ProcessingService : IEventListenerV2ProcessingService
    {
        private readonly IEventListenerV2Service eventListenerV2Service;
        private readonly ILoggingBroker loggingBroker;

        public EventListenerV2ProcessingService(
            IEventListenerV2Service eventListenerV2Service,
            ILoggingBroker loggingBroker)
        {
            this.eventListenerV2Service = eventListenerV2Service;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IQueryable<EventListenerV2>> RetrieveEventListenerV2sByEventAddressIdAsync(
            Guid eventAddressId) => TryCatch(async () =>
        {
            ValidateEventAddressId(eventAddressId);

            IQueryable<EventListenerV2> eventListenerV2s =
                await this.eventListenerV2Service.RetrieveAllEventListenerV2sAsync();

            return eventListenerV2s.Where(eventListenerV2 =>
                eventListenerV2.EventAddressId == eventAddressId);
        });
    }
}
