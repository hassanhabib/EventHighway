// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using EventHighway.Core.Models.EventListeners;
using EventHighway.Core.Services.Foundations.EventListeners;
using EventHighway.Core.Services.Processings.EventListeners;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventListeners
{
    public partial class EventListenerProcessingServiceTests
    {
        private readonly Mock<IEventListenerService> eventListenerServiceMock;
        private readonly IEventListenerProcessingService eventListenerProcessingService;

        public EventListenerProcessingServiceTests()
        {
            this.eventListenerServiceMock =
                new Mock<IEventListenerService>();

            this.eventListenerProcessingService =
                new EventListenerProcessingService(
                    eventListenerService: eventListenerServiceMock.Object);
        }

        private static IQueryable<EventListener> CreateRandomEventListeners() =>
            CreateEventListenerFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static IQueryable<EventListener> CreateRandomEventListeners(
            Guid eventAddressId)
        {
            IQueryable<EventListener> randomEventListeners =
                CreateRandomEventListeners();

            randomEventListeners.ToList().ForEach(listener =>
            {
                listener.EventAddressId = eventAddressId;
            });

            return randomEventListeners;
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<EventListener> CreateEventListenerFiller()
        {
            var filler = new Filler<EventListener>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset);

            return filler;
        }
    }
}
