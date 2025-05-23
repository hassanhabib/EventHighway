﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Services.Foundations.Events.V1;

namespace EventHighway.Core.Services.Processings.Events.V1
{
    internal partial class EventV1ProcessingService : IEventV1ProcessingService
    {
        private readonly IEventV1Service eventV1Service;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventV1ProcessingService(
            IEventV1Service eventV1Service,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.eventV1Service = eventV1Service;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventV1> AddEventV1Async(EventV1 eventV1) =>
        TryCatch(async () =>
        {
            ValidateEventV1IsNotNull(eventV1);

            return await this.eventV1Service.AddEventV1Async(eventV1);
        });

        public ValueTask<IQueryable<EventV1>> RetrieveScheduledPendingEventV1sAsync() =>
        TryCatch(async () =>
        {
            IQueryable<EventV1> eventV1s =
                await this.eventV1Service.RetrieveAllEventV1sAsync();

            DateTimeOffset now =
                await this.dateTimeBroker.GetDateTimeOffsetAsync();

            return eventV1s.Where(eventV1 =>
                eventV1.Type == EventV1Type.Scheduled &&
                eventV1.ScheduledDate < now);
        });

        public ValueTask<EventV1> ModifyEventV1Async(EventV1 eventV1)
        {
            throw new NotImplementedException();
        }

        public ValueTask<EventV1> RemoveEventV1ByIdAsync(Guid eventV1Id) =>
        TryCatch(async () =>
        {
            ValidateEventV1Id(eventV1Id);

            return await this.eventV1Service.RemoveEventV1ByIdAsync(
                eventV1Id);
        });
    }
}
