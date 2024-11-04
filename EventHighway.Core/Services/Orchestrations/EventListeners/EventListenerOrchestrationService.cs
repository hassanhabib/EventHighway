// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.EventCall;
using EventHighway.Core.Models.EventListeners;
using EventHighway.Core.Models.Events;
using EventHighway.Core.Models.ListenerEvents;
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
                var listenerEvent = new ListenerEvent
                {
                    Id = Guid.NewGuid(),
                    EventId = @event.Id,
                    EventListenerId = listener.Id,
                    EventAddressId = @event.EventAddressId,
                    Status = ListenerEventStatus.Pending,
                    CreatedDate = @event.CreatedDate,
                    UpdatedDate = @event.CreatedDate,
                };

                ListenerEvent addedListenerEvent =
                    await this.listenerEventProcessingService
                        .AddListenerEventAsync(listenerEvent);

                var eventCall = new EventCall
                {
                    Content = @event.Content,
                    Endpoint = listener.Endpoint,
                    Response = null
                };

                EventCall ranEventCall =
                    await this.eventCallProcessingService
                        .RunAsync(eventCall);

                addedListenerEvent.Response = ranEventCall.Response;

                addedListenerEvent.UpdatedDate =
                    await this.dateTimeBroker.GetDateTimeOffsetAsync();

                addedListenerEvent.Status = ListenerEventStatus.Completed;

                await this.listenerEventProcessingService
                    .ModifyListenerEventAsync(addedListenerEvent);
            }

            return @event;
        }
    }
}
