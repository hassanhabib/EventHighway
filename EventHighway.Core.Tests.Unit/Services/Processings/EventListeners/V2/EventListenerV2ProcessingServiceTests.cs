// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions;
using EventHighway.Core.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Services.Processings.EventListeners.V2;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventListeners.V2
{
    public partial class EventListenerV2ProcessingServiceTests
    {
        private readonly Mock<IEventListenerV1Service> eventListenerV2ServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventListenerV2ProcessingService eventListenerV2ProcessingService;

        public EventListenerV2ProcessingServiceTests()
        {
            this.eventListenerV2ServiceMock =
                new Mock<IEventListenerV1Service>();

            this.loggingBrokerMock =
                new Mock<ILoggingBroker>();

            this.eventListenerV2ProcessingService =
                new EventListenerV2ProcessingService(
                    eventListenerV2Service: eventListenerV2ServiceMock.Object,
                    loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventListenerV1ValidationException(
                    someMessage,
                    someInnerException),

                new EventListenerV1DependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }
        
        public static TheoryData<Xeption> DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventListenerV1DependencyException(
                    someMessage,
                    someInnerException),

                new EventListenerV1ServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static IQueryable<EventListenerV1> CreateRandomEventListenerV2s() =>
            CreateEventListenerV2Filler().Create(count: GetRandomNumber()).AsQueryable();

        private static EventListenerV1 CreateRandomEventListenerV2() =>
            CreateEventListenerV2Filler().Create();

        private static IQueryable<EventListenerV1> CreateRandomEventListenerV2s(
            Guid eventAddressId)
        {
            IQueryable<EventListenerV1> randomEventListenerV2s =
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

        private static Filler<EventListenerV1> CreateEventListenerV2Filler()
        {
            var filler = new Filler<EventListenerV1>();

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
