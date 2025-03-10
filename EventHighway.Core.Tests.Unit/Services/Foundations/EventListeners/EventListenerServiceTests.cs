// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.Services.Foundations.EventListeners;
using EventHighway.Core.Services.Foundations.EventListeners;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.EventListeners
{
    public partial class EventListenerServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IEventListenerService eventListenerService;

        public EventListenerServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.eventListenerService = new EventListenerService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static EventListener CreateRandomEventListener() =>
            CreateEventListenerFiller().Create();

        private static IQueryable<EventListener> CreateRandomEventListeners() =>
            CreateEventListenerFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset CreateRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<EventListener> CreateEventListenerFiller()
        {
            var filler = new Filler<EventListener>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(CreateRandomDateTime);

            return filler;
        }
    }
}
