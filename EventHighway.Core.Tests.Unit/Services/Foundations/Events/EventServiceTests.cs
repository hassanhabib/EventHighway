// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.Events;
using EventHighway.Core.Services.Foundations.Events;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.Events
{
    public partial class EventServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IEventService eventService;

        public EventServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.eventService = new EventService(
                storageBroker: this.storageBrokerMock.Object);
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
