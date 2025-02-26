// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
            Console.WriteLine($"Start time: {DateTimeOffset.UtcNow}");

            if (@event.PublishedDate is not null)
            {
                TimeSpan delayTime =
                    @event.PublishedDate.Value - 
                        DateTimeOffset.UtcNow;

                _ = DelayEventAsync(@event, delayTime);

                return @event; 
            }

            return await ProcessEventAsync(@event);
        }

        private async Task DelayEventAsync(Event @event, TimeSpan delay)
        {
            await Task.Delay(delay);
            await ProcessEventAsync(@event);
        }

        private async ValueTask<Event> ProcessEventAsync(Event @event)
        {
            Console.WriteLine($"Published time: {DateTimeOffset.UtcNow}");

            Event submittedEvent =
                await this.eventOrchestrationService.SubmitEventAsync(
                    @event);

            return await this.eventListenerOrchestrationService
                .SubmitEventToListenersAsync(submittedEvent);
        }
    }
}
