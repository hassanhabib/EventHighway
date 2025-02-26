﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Events;
using EventHighway.Core.Services.Coordinations.Events;
using EventHighway.Core.Services.Orchestrations.EventListeners;
using EventHighway.Core.Services.Orchestrations.Events;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations
{
    public partial class EventCoordinationServiceTests
    {
        private readonly Mock<IEventOrchestrationService> eventOrchestrationServiceMock;
        private readonly Mock<IEventListenerOrchestrationService> eventListenerOrchestrationServiceMock;
        private readonly IEventCoordinationService eventCoordinationService;

        public EventCoordinationServiceTests()
        {
            this.eventOrchestrationServiceMock =
                new Mock<IEventOrchestrationService>();

            this.eventListenerOrchestrationServiceMock =
                new Mock<IEventListenerOrchestrationService>();

            this.eventCoordinationService = new EventCoordinationService(
                eventOrchestrationService: this.eventOrchestrationServiceMock.Object,
                eventListenerOrchestrationService: this.eventListenerOrchestrationServiceMock.Object);
        }

        private static Event CreateRandomEvent() =>
            CreateEventFiller().Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset CreateRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Event> CreateEventFiller()
        {
            var filler = new Filler<Event>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(CreateRandomDateTime)
                .OnType<DateTimeOffset?>().IgnoreIt();

            return filler;
        }
    }
}
