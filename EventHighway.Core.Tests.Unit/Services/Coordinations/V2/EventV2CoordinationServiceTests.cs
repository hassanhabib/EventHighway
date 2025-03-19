// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions;
using EventHighway.Core.Services.Coordinations.Events.V2;
using EventHighway.Core.Services.Orchestrations.EventListeners.V2;
using EventHighway.Core.Services.Orchestrations.Events.V2;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        private readonly Mock<IEventV2OrchestrationService> eventV2OrchestrationServiceMock;
        private readonly Mock<IEventListenerV2OrchestrationService> eventListenerV2OrchestrationServiceMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IEventV2CoordinationService eventV2CoordinationService;

        public EventV2CoordinationServiceTests()
        {
            this.eventV2OrchestrationServiceMock =
                new Mock<IEventV2OrchestrationService>();

            this.eventListenerV2OrchestrationServiceMock =
                new Mock<IEventListenerV2OrchestrationService>();

            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            var compareConfiguration = new ComparisonConfig();

            compareConfiguration.IgnoreProperty<ListenerEventV2>(listenerEventV2 =>
                listenerEventV2.Id);

            this.compareLogic = new CompareLogic(compareConfiguration);

            this.eventV2CoordinationService =
                new EventV2CoordinationService(
                    eventV2OrchestrationService: this.eventV2OrchestrationServiceMock.Object,
                    eventListenerV2OrchestrationService: this.eventListenerV2OrchestrationServiceMock.Object,
                    dateTimeBroker: this.dateTimeBrokerMock.Object,
                    loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> EventV2ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventV2OrchestrationValidationException(
                    someMessage,
                    someInnerException),

                new EventV2OrchestrationDependencyValidationException(
                    someMessage,
                    someInnerException)
            };
        }

        public static TheoryData<Xeption> EventV2DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventV2OrchestrationDependencyException(
                    someMessage,
                    someInnerException),

                new EventV2OrchestrationServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> EventListenerV2ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventListenerV2OrchestrationValidationException(
                    someMessage,
                    someInnerException),

                new EventListenerV2OrchestrationDependencyValidationException(
                    someMessage,
                    someInnerException)
            };
        }

        public static TheoryData<Xeption> EventListenerV2DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventListenerV2OrchestrationDependencyException(
                    someMessage,
                    someInnerException),

                new EventListenerV2OrchestrationServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<DateTimeOffset, DateTimeOffset?> ScheduledDates()
        {
            int randomNegativeDays = GetRandomNegativeNumber();
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();

            DateTimeOffset scheduledNegativeDate =
                randomDateTimeOffset.AddDays(randomNegativeDays);

            return new TheoryData<DateTimeOffset, DateTimeOffset?>
            {
                {
                    randomDateTimeOffset,
                    scheduledNegativeDate
                },
                {
                    randomDateTimeOffset,
                    null
                }
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static IQueryable<EventV2> CreateRandomEventV2s()
        {
            return CreateEventV2Filler()
                .Create(count: GetRandomNumber())
                    .AsQueryable();
        }

        private static EventV2 CreateRandomEventV2() =>
            CreateEventV2Filler().Create();

        private static IQueryable<EventListenerV2> CreateRandomEventListenerV2s() =>
            CreateEventListenerV2Filler().Create(count: GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static int GetRandomNegativeNumber() =>
            -1 * GetRandomNumber();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private Expression<Func<ListenerEventV2, bool>> SameListenerEventAs(
           ListenerEventV2 expectedListenerEventV2)
        {
            return actualListenerEventV2 =>
                this.compareLogic.Compare(
                    expectedListenerEventV2,
                    actualListenerEventV2)
                        .AreEqual;
        }

        private Expression<Func<EventCallV2, bool>> SameEventCallAs(
           EventCallV2 expectedEventCallV2)
        {
            return actualEventCallV2 =>
                this.compareLogic.Compare(
                    expectedEventCallV2,
                    actualEventCallV2)
                        .AreEqual;
        }

        private static Filler<EventV2> CreateEventV2Filler()
        {
            var filler = new Filler<EventV2>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(GetRandomDateTimeOffset)

                .OnType<DateTimeOffset?>()
                    .Use(GetRandomDateTimeOffset())

                .OnProperty(eventV2 =>
                    eventV2.EventAddress).IgnoreIt()

                .OnProperty(eventV2 =>
                    eventV2.ListenerEvents).IgnoreIt();

            return filler;
        }

        private static Filler<EventListenerV2> CreateEventListenerV2Filler()
        {
            var filler = new Filler<EventListenerV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.EventAddress).IgnoreIt()

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.ListenerEvents).IgnoreIt();

            return filler;
        }
    }
}
