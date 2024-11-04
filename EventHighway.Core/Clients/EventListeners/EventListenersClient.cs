// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;
using EventHighway.Core.Services.Foundations.EventListeners;

namespace EventHighway.Core.Clients.EventListeners
{
    public class EventListenersClient : IEventListenersClient
    {
        private readonly IEventListenerService eventListenerService;

        public EventListenersClient(IEventListenerService eventListenerService) =>
            this.eventListenerService = eventListenerService;

        public async ValueTask<IQueryable<EventListener>> GetAllEventListenersAsync() =>
            await this.eventListenerService.RetrieveAllEventListenersAsync();

        public async ValueTask<EventListener> RegisterEventListenerAsync(EventListener eventListener) =>
            await this.eventListenerService.AddEventListenerAsync(eventListener);
    }
}
