// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Events;
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

        public ValueTask<Event> SubmitEventToListenersAsync(Event @event)
        {
            throw new NotImplementedException();
        }
    }
}
