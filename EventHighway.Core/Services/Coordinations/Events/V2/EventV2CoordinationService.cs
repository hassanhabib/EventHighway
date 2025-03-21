// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Services.Orchestrations.EventListeners.V2;
using EventHighway.Core.Services.Orchestrations.Events.V2;

namespace EventHighway.Core.Services.Coordinations.Events.V2
{
    internal partial class EventV2CoordinationService : IEventV2CoordinationService
    {
        private readonly IEventV2OrchestrationService eventV2OrchestrationService;
        private readonly IEventListenerV2OrchestrationService eventListenerV2OrchestrationService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventV2CoordinationService(
            IEventV2OrchestrationService eventV2OrchestrationService,
            IEventListenerV2OrchestrationService eventListenerV2OrchestrationService,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.eventV2OrchestrationService = eventV2OrchestrationService;
            this.eventListenerV2OrchestrationService = eventListenerV2OrchestrationService;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventV2> SubmitEventV2Async(EventV2 eventV2) =>
        TryCatch(async () =>
        {
            ValidateEventV2IsNotNull(eventV2);

            DateTimeOffset now =
                await this.dateTimeBroker.GetDateTimeOffsetAsync();

            eventV2.Type = eventV2.ScheduledDate switch
            {
                null => EventV2Type.Immediate,

                DateTimeOffset scheduledDate
                    when scheduledDate < now => EventV2Type.Immediate,

                _ => EventV2Type.Scheduled,
            };

            EventV2 submittedEventV2 =
                await this.eventV2OrchestrationService
                    .SubmitEventV2Async(eventV2);

            if (submittedEventV2.Type is EventV2Type.Immediate)
                await ProcessEventListenersAsync(submittedEventV2);

            return submittedEventV2;
        });

        public ValueTask FireScheduledPendingEventV2sAsync() =>
        TryCatch(async () =>
        {
            IQueryable<EventV2> eventV2s =
                await this.eventV2OrchestrationService
                    .RetrieveScheduledPendingEventV2sAsync();

            foreach (EventV2 eventV2 in eventV2s)
            {
                await ProcessEventListenersAsync(eventV2);
            }
        });

        private async ValueTask ProcessEventListenersAsync(EventV2 eventV2)
        {
            IQueryable<EventListenerV2> eventListenerV2s =
                await this.eventListenerV2OrchestrationService
                    .RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId);

            foreach (EventListenerV2 eventListenerV2 in eventListenerV2s)
            {
                ListenerEventV2 listenerEventV2 =
                    CreateEventListenerV2(eventV2, eventListenerV2);

                ListenerEventV2 addedListenerEventV2 =
                    await this.eventListenerV2OrchestrationService
                        .AddListenerEventV2Async(listenerEventV2);

                await RunEventCallAsync(
                    eventV2,
                    eventListenerV2,
                    addedListenerEventV2);
            }
        }

        public ValueTask<EventV2> RemoveEventV2ByIdAsync(Guid eventV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventV2Id(eventV2Id);

            return await this.eventV2OrchestrationService
                .RemoveEventV2ByIdAsync(eventV2Id);
        });

        private async Task RunEventCallAsync(
            EventV2 eventV2,
            EventListenerV2 eventListenerV2,
            ListenerEventV2 listenerEventV2)
        {
            var eventCallV2 = new EventCallV2
            {
                Content = eventV2.Content,
                Endpoint = eventListenerV2.Endpoint,
                Secret = eventListenerV2.HeaderSecret,
                Response = null
            };

            try
            {
                EventCallV2 ranEventCallV2 =
                    await this.eventV2OrchestrationService
                        .RunEventCallV2Async(eventCallV2);

                listenerEventV2.Response = ranEventCallV2.Response;
                listenerEventV2.Status = ListenerEventV2Status.Success;
            }
            catch (Exception exception)
            {
                listenerEventV2.Response = exception.Message;
                listenerEventV2.Status = ListenerEventV2Status.Error;
            }

            listenerEventV2.UpdatedDate =
                await this.dateTimeBroker.GetDateTimeOffsetAsync();

            await this.eventListenerV2OrchestrationService
                .ModifyListenerEventV2Async(listenerEventV2);
        }

        private static ListenerEventV2 CreateEventListenerV2(
            EventV2 eventV2,
            EventListenerV2 eventListenerV2)
        {
            return new ListenerEventV2
            {
                Id = Guid.NewGuid(),
                EventId = eventV2.Id,
                EventListenerId = eventListenerV2.Id,
                EventAddressId = eventV2.EventAddressId,
                Status = ListenerEventV2Status.Pending,
                CreatedDate = eventV2.CreatedDate,
                UpdatedDate = eventV2.UpdatedDate,
            };
        }
    }
}
