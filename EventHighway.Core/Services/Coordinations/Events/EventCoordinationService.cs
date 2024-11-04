// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events;
using EventHighway.Core.Services.Orchestrations.EventListeners;
using EventHighway.Core.Services.Orchestrations.Events;

namespace EventHighway.Core.Services.Coordinations.Events
{
    internal class EventCoordinationService : IEventCoordinationService
    {
        private readonly IEventOrchestrationService eventOrchestrationService;
        private readonly IEventListenerOrchestrationService eventListenerOrchestrationService;

        public EventCoordinationService(
            IEventOrchestrationService eventOrchestrationService,
            IEventListenerOrchestrationService eventListenerOrchestrationService)
        {
            this.eventOrchestrationService = eventOrchestrationService;
            this.eventListenerOrchestrationService = eventListenerOrchestrationService;
        }

        public async ValueTask<Event> SubmitEventAsync(Event @event)
        {
            Event submittedEvent =
                await this.eventOrchestrationService
                    .SubmitEventAsync(@event);

            return await this.eventListenerOrchestrationService
                .SubmitEventToListenersAsync(submittedEvent);
        }
    }
}
