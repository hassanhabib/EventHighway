// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Processings.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions;
using EventHighway.Core.Services.Orchestrations.EventListeners.V2;
using EventHighway.Core.Services.Processings.EventListeners.V2;
using EventHighway.Core.Services.Processings.ListenerEvents.V2;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        private readonly Mock<IEventListenerV2ProcessingService> eventListenerV2ProcessingServiceMock;
        private readonly Mock<IListenerEventV2ProcessingService> listenerEventV2ProcessingServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventListenerV2OrchestrationService eventListenerV2OrchestrationService;

        public EventListenerV2OrchestrationServiceTests()
        {
            this.eventListenerV2ProcessingServiceMock =
                new Mock<IEventListenerV2ProcessingService>();

            this.listenerEventV2ProcessingServiceMock =
                new Mock<IListenerEventV2ProcessingService>();

            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.eventListenerV2OrchestrationService =
                new EventListenerV2OrchestrationService(
                    eventListenerV2ProcessingService: this.eventListenerV2ProcessingServiceMock.Object,
                    listenerEventV2ProcessingService: this.listenerEventV2ProcessingServiceMock.Object,
                    loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> EventListenerV2DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventListenerV2ProcessingDependencyException(
                    someMessage,
                    someInnerException),

                new EventListenerV2ProcessingServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> ListenerEventV2ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new ListenerEventV2ProcessingValidationException(
                    someMessage,
                    someInnerException),

                new ListenerEventV2ProcessingDependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> ListenerEventV2DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new ListenerEventV2ProcessingDependencyException(
                    someMessage,
                    someInnerException),

                new ListenerEventV2ProcessingServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Guid GetRandomId() =>
            Guid.NewGuid();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static IQueryable<ListenerEventV2> CreateRandomListenerEventV2s() =>
            CreateListenerEventV2Filler().Create(count: GetRandomNumber()).AsQueryable();

        private static ListenerEventV2 CreateRandomListenerEventV2() =>
            CreateListenerEventV2Filler().Create();

        private static IQueryable<EventListenerV2> CreateRandomEventListenerV2s() =>
            CreateEventListenerV2Filler().Create(count: GetRandomNumber()).AsQueryable();

        private static EventListenerV2 CreateRandomEventListenerV2() =>
            CreateEventListenerV2Filler().Create();

        private static Filler<ListenerEventV2> CreateListenerEventV2Filler()
        {
            var filler = new Filler<ListenerEventV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)

                .OnProperty(listenerEventV2 => listenerEventV2.Event)
                    .IgnoreIt()

                .OnProperty(listenerEventV2 => listenerEventV2.EventAddress)
                    .IgnoreIt()

                .OnProperty(listenerEventV2 => listenerEventV2.EventListener)
                    .IgnoreIt();

            return filler;
        }

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
