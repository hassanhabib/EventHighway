// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Services.Foundations.Events.V2;

namespace EventHighway.Core.Services.Processings.Events.V2
{
    internal partial class EventV2ProcessingService : IEventV2ProcessingService
    {
        private readonly IEventV2Service eventV2Service;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventV2ProcessingService(
            IEventV2Service eventV2Service,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.eventV2Service = eventV2Service;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventV1> AddEventV2Async(EventV1 eventV2) =>
        TryCatch(async () =>
        {
            ValidateEventV2IsNotNull(eventV2);

            return await this.eventV2Service.AddEventV2Async(eventV2);
        });

        public ValueTask<IQueryable<EventV1>> RetrieveScheduledPendingEventV2sAsync() =>
        TryCatch(async () =>
        {
            IQueryable<EventV1> eventV2s =
                await this.eventV2Service.RetrieveAllEventV2sAsync();

            DateTimeOffset now =
                await this.dateTimeBroker.GetDateTimeOffsetAsync();

            return eventV2s.Where(eventV2 =>
                eventV2.Type == EventV1Type.Scheduled &&
                eventV2.ScheduledDate < now);
        });

        public ValueTask<EventV1> RemoveEventV2ByIdAsync(Guid eventV2Id) =>
        TryCatch(async () =>
        {
            ValidateEventV2Id(eventV2Id);

            return await this.eventV2Service.RemoveEventV2ByIdAsync(
                eventV2Id);
        });
    }
}
