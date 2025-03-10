// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.EventCall;
using EventHighway.Core.Models.Services.Foundations.EventListeners;
using EventHighway.Core.Models.Services.Foundations.Events;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents;
using EventHighway.Core.Services.Processings.EventCalls;
using EventHighway.Core.Services.Processings.EventListeners;
using EventHighway.Core.Services.Processings.ListenerEvents;

namespace EventHighway.Core.Services.Orchestrations.EventListeners
{
    internal class EventListenerOrchestrationService : IEventListenerOrchestrationService
    {
        private readonly IEventListenerProcessingService eventListenerProcessingService;
        private readonly IListenerEventProcessingService listenerEventProcessingService;
        private readonly IEventCallProcessingService eventCallProcessingService;
        private readonly IDateTimeBroker dateTimeBroker;

        public EventListenerOrchestrationService(
            IEventListenerProcessingService eventListenerProcessingService,
            IListenerEventProcessingService listenerEventProcessingService,
            IEventCallProcessingService eventCallProcessingService,
            IDateTimeBroker dateTimeBroker)
        {
            this.eventListenerProcessingService = eventListenerProcessingService;
            this.listenerEventProcessingService = listenerEventProcessingService;
            this.eventCallProcessingService = eventCallProcessingService;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async ValueTask<Event> SubmitEventToListenersAsync(Event @event)
        {
            IQueryable<EventListener> eventListeners =
                await this.eventListenerProcessingService
                    .RetrieveEventListenersByAddressIdAsync(@event.EventAddressId);

            foreach (EventListener listener in eventListeners)
            {
                ListenerEvent listenerEvent =
                    CreateEventListener(@event, listener);

                ListenerEvent addedListenerEvent =
                    await this.listenerEventProcessingService
                        .AddListenerEventAsync(listenerEvent);

                await RunEventCallAsync(@event, listener, addedListenerEvent);
            }

            return @event;
        }

        private async Task RunEventCallAsync(
            Event @event,
            EventListener listener,
            ListenerEvent addedListenerEvent)
        {
            var eventCall = new EventCall
            {
                Content = @event.Content,
                Endpoint = listener.Endpoint,
                Secret = listener.HeaderSecret,
                Response = null
            };

            try
            {
                EventCall ranEventCall =
                    await this.eventCallProcessingService
                        .RunAsync(eventCall);

                addedListenerEvent.Response = ranEventCall.Response;
            }
            catch (Exception exception)
            {
                addedListenerEvent.Response = exception.Message;
            }

            addedListenerEvent.UpdatedDate =
                    await this.dateTimeBroker.GetDateTimeOffsetAsync();

            addedListenerEvent.Status = ListenerEventStatus.Completed;

            await this.listenerEventProcessingService
                .ModifyListenerEventAsync(addedListenerEvent);
        }

        private static ListenerEvent CreateEventListener(Event @event, EventListener listener)
        {
            return new ListenerEvent
            {
                Id = Guid.NewGuid(),
                EventId = @event.Id,
                EventListenerId = listener.Id,
                EventAddressId = @event.EventAddressId,
                Status = ListenerEventStatus.Pending,
                CreatedDate = @event.CreatedDate,
                UpdatedDate = @event.CreatedDate,
            };
        }
    }
}
