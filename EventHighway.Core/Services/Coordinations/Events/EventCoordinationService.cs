// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading;
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

        public ValueTask<Event> SubmitEventAsync(Event @event)
        {
            return @event.PublishedDate is not null
                ? HandleDelayedEvent(@event)
                : ProcessEventAsync(@event);
        }

        private ValueTask<Event> HandleDelayedEvent(Event @event)
        {
            TimeSpan delayTime =
                @event.PublishedDate.Value - DateTimeOffset.UtcNow;

            ScheduleEvent(@event, delayTime);

            return new ValueTask<Event>(@event); 
        }

        private void ScheduleEvent(Event @event, TimeSpan delay)
        {
            var timer = new Timer(async _ =>
            {
                await ProcessEventAsync(@event);
            });

            timer.Change(
                dueTime: delay, 
                period: Timeout.InfiniteTimeSpan);
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
