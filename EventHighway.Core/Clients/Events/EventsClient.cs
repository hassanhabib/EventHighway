// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events;
using EventHighway.Core.Services.Coordinations.Events;

namespace EventHighway.Core.Clients.Events
{
    public class EventsClient : IEventsClient
    {
        private readonly IEventCoordinationService eventCoordinationService;

        public EventsClient(IEventCoordinationService eventCoordinationService) =>
            this.eventCoordinationService = eventCoordinationService;

        public async ValueTask<Event> SubmitEventAsync(Event @event) =>
            await this.eventCoordinationService.SubmitEventAsync(@event);
    }
}
