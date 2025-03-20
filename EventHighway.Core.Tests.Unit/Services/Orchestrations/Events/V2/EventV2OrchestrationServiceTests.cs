// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Models.Services.Processings.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Processings.EventCalls.V2.Exceptions;
using EventHighway.Core.Models.Services.Processings.Events.V2.Exceptions;
using EventHighway.Core.Services.Orchestrations.Events.V2;
using EventHighway.Core.Services.Processings.EventAddresses.V2;
using EventHighway.Core.Services.Processings.EventCalls.V2;
using EventHighway.Core.Services.Processings.Events.V2;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        private readonly Mock<IEventV2ProcessingService> eventV2ProcessingServiceMock;
        private readonly Mock<IEventAddressV2ProcessingService> eventAddressV2ProcessingServiceMock;
        private readonly Mock<IEventCallV2ProcessingService> eventCallV2ProcessingServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventV2OrchestrationService eventV2OrchestrationService;

        public EventV2OrchestrationServiceTests()
        {
            this.eventV2ProcessingServiceMock =
                new Mock<IEventV2ProcessingService>();

            this.eventAddressV2ProcessingServiceMock =
                new Mock<IEventAddressV2ProcessingService>();

            this.eventCallV2ProcessingServiceMock =
                new Mock<IEventCallV2ProcessingService>();

            this.loggingBrokerMock =
                new Mock<ILoggingBroker>();

            this.eventV2OrchestrationService =
                new EventV2OrchestrationService(
                    eventV2ProcessingService: this.eventV2ProcessingServiceMock.Object,
                    eventAddressV2ProcessingService: this.eventAddressV2ProcessingServiceMock.Object,
                    eventCallV2ProcessingService: this.eventCallV2ProcessingServiceMock.Object,
                    loggingBroker: loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> EventCallV2ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventCallV2ProcessingValidationException(
                    someMessage,
                    someInnerException),

                new EventCallV2ProcessingDependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> EventCallV2DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventCallV2ProcessingDependencyException(
                    someMessage,
                    someInnerException),

                new EventCallV2ProcessingServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> EventAddressV2ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventAddressV2ProcessingValidationException(
                    someMessage,
                    someInnerException),

                new EventAddressV2ProcessingDependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> EventAddressV2DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventAddressV2ProcessingDependencyException(
                    someMessage,
                    someInnerException),

                new EventAddressV2ProcessingServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> EventV2ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventV2ProcessingValidationException(
                    someMessage,
                    someInnerException),

                new EventV2ProcessingDependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> EventV2DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventV2ProcessingDependencyException(
                    someMessage,
                    someInnerException),

                new EventV2ProcessingServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Guid GetRandomId() =>
            Guid.NewGuid();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset()
        {
            return new DateTimeRange(
                earliestDate: DateTime.UnixEpoch)
                    .GetValue();
        }

        private static EventCallV2 CreateRandomEventCallV2() =>
            CreateEventCallV2Filler().Create();

        private static IQueryable<EventV2> CreateRandomEventV2s()
        {
            return CreateEventV2Filler()
                .Create(count: GetRandomNumber())
                    .AsQueryable();
        }

        private static EventAddressV2 CreateRandomEventAddressV2() =>
            CreateEventAddressV2Filler().Create();

        private static EventV2 CreateRandomEventV2() =>
            CreateEventV2Filler().Create();

        private static Filler<EventCallV2> CreateEventCallV2Filler() =>
            new Filler<EventCallV2>();

        private static Filler<EventAddressV2> CreateEventAddressV2Filler()
        {
            var filler = new Filler<EventAddressV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)

                .OnProperty(eventAddressV2 => eventAddressV2.Events)
                    .IgnoreIt()

                .OnProperty(eventAddressV2 => eventAddressV2.EventListeners)
                    .IgnoreIt()

                .OnProperty(eventAddressV2 => eventAddressV2.ListenerEvents)
                    .IgnoreIt();

            return filler;
        }

        private static Filler<EventV2> CreateEventV2Filler()
        {
            var filler = new Filler<EventV2>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(GetRandomDateTimeOffset)

                .OnType<DateTimeOffset?>()
                    .Use(GetRandomDateTimeOffset());

            return filler;
        }
    }
}
