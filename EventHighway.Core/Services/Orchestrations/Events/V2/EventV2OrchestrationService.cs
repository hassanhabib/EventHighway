// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Services.Processings.EventAddresses.V2;
using EventHighway.Core.Services.Processings.EventCalls.V2;
using EventHighway.Core.Services.Processings.Events.V2;

namespace EventHighway.Core.Services.Orchestrations.Events.V2
{
    internal partial class EventV2OrchestrationService : IEventV2OrchestrationService
    {
        private readonly IEventV2ProcessingService eventV2ProcessingService;
        private readonly IEventAddressV2ProcessingService eventAddressV2ProcessingService;
        private readonly IEventCallV2ProcessingService eventCallV2ProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public EventV2OrchestrationService(
            IEventV2ProcessingService eventV2ProcessingService,
            IEventAddressV2ProcessingService eventAddressV2ProcessingService,
            IEventCallV2ProcessingService eventCallV2ProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.eventV2ProcessingService = eventV2ProcessingService;
            this.eventAddressV2ProcessingService = eventAddressV2ProcessingService;
            this.eventCallV2ProcessingService = eventCallV2ProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventV1> SubmitEventV2Async(EventV1 eventV2) =>
        TryCatch(async () =>
        {
            ValidateEventV2IsNotNull(eventV2);

            EventAddressV1 maybeEventAddressV2 =
                await this.eventAddressV2ProcessingService
                    .RetrieveEventAddressV2ByIdAsync(
                        eventV2.EventAddressId);

            ValidateListenerEventV2Exists(
                maybeEventAddressV2,
                eventV2.EventAddressId);

            return await this.eventV2ProcessingService
                .AddEventV2Async(eventV2);
        });

        public ValueTask<IQueryable<EventV1>> RetrieveScheduledPendingEventV2sAsync() =>
        TryCatch(async () =>
        {
            return await this.eventV2ProcessingService
                .RetrieveScheduledPendingEventV2sAsync();
        });

        public ValueTask<EventV1> RemoveEventV2ByIdAsync(Guid eventV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventV2Id(eventV2Id);

            return await this.eventV2ProcessingService
                .RemoveEventV2ByIdAsync(eventV2Id);
        });

        public ValueTask<EventCallV1> RunEventCallV2Async(EventCallV1 eventCallV2) =>
        TryCatch(async () =>
        {
            ValidateEventCallV2IsNotNull(eventCallV2);

            return await this.eventCallV2ProcessingService.RunEventCallV2Async(
                eventCallV2);
        });
    }
}
