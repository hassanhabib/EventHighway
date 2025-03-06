// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Services.Processings.EventListeners.V2;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventListeners.V2
{
    public partial class EventListenerV2ProcessingServiceTests
    {
        private readonly Mock<IEventListenerV2Service> eventListenerV2ServiceMock;
        private readonly IEventListenerV2ProcessingService eventListenerV2ProcessingService;

        public EventListenerV2ProcessingServiceTests()
        {
            this.eventListenerV2ServiceMock =
                new Mock<IEventListenerV2Service>();

            this.eventListenerV2ProcessingService =
                new EventListenerV2ProcessingService(
                    eventListenerV2Service: eventListenerV2ServiceMock.Object);
        }

        private static IQueryable<EventListenerV2> CreateRandomEventListenerV2s() =>
            CreateEventListenerV2Filler().Create(count: GetRandomNumber()).AsQueryable();

        private static IQueryable<EventListenerV2> CreateRandomEventListenerV2s(
            Guid eventAddressId)
        {
            IQueryable<EventListenerV2> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            randomEventListenerV2s.ToList().ForEach(listener =>
            {
                listener.EventAddressId = eventAddressId;
            });

            return randomEventListenerV2s;
        }

        private static Guid GetRandomId() =>
            Guid.NewGuid();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Filler<EventListenerV2> CreateEventListenerV2Filler()
        {
            var filler = new Filler<EventListenerV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)

                .OnProperty(eventListenerV2 => eventListenerV2.EventAddress)
                    .IgnoreIt()

                .OnProperty(eventListenerV2 => eventListenerV2.ListenerEvents)
                    .IgnoreIt();

            return filler;
        }
    }
}
