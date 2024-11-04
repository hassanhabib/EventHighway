// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Events;
using EventHighway.Core.Services.Foundations.EventAddresses;
using EventHighway.Core.Services.Foundations.Events;
using EventHighway.Core.Services.Orchestrations.Events;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events
{
    public partial class EventOrchestrationServiceTests
    {
        private readonly Mock<IEventAddressService> eventAddressServiceMock;
        private readonly Mock<IEventService> eventServiceMock;
        private readonly IEventOrchestrationService eventOrchestrationService;

        public EventOrchestrationServiceTests()
        {
            this.eventAddressServiceMock = new Mock<IEventAddressService>();
            this.eventServiceMock = new Mock<IEventService>();

            this.eventOrchestrationService = new EventOrchestrationService(
                eventAddressService: this.eventAddressServiceMock.Object,
                eventService: this.eventServiceMock.Object);
        }

        private static Event CreateRandomEvent() =>
            CreateEventFiller().Create();

        private static DateTimeOffset CreateRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Event> CreateEventFiller()
        {
            var filler = new Filler<Event>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(CreateRandomDateTime);

            return filler;
        }
    }
}
