// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;
using EventHighway.Core.Services.Foundations.EventListeners;

namespace EventHighway.Core.Services.Processings.EventListeners
{
    internal class EventListenerProcessingService : IEventListenerProcessingService
    {
        private readonly IEventListenerService eventListenerService;

        public EventListenerProcessingService(IEventListenerService eventListenerService) =>
            this.eventListenerService = eventListenerService;

        public async ValueTask<IQueryable<EventListener>> RetrieveEventListenersByAddressIdAsync(Guid addressId)
        {
            IQueryable<EventListener> storageEventListeners =
                await this.eventListenerService.RetrieveAllEventListenersAsync();

            return storageEventListeners.Where(eventListener => eventListener.EventAddressId == addressId);
        }
    }
}
