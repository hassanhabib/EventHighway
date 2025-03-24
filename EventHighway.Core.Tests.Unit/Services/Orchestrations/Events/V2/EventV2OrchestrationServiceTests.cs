// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Processings.EventAddresses.V1.Exceptions;
using EventHighway.Core.Models.Services.Processings.EventCalls.V1.Exceptions;
using EventHighway.Core.Models.Services.Processings.Events.V1.Exceptions;
using EventHighway.Core.Services.Orchestrations.Events.V2;
using EventHighway.Core.Services.Processings.EventAddresses.V1;
using EventHighway.Core.Services.Processings.EventCalls.V1;
using EventHighway.Core.Services.Processings.Events.V1;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        private readonly Mock<IEventV1ProcessingService> eventV2ProcessingServiceMock;
        private readonly Mock<IEventAddressV1ProcessingService> eventAddressV2ProcessingServiceMock;
        private readonly Mock<IEventCallV1ProcessingService> eventCallV2ProcessingServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventV2OrchestrationService eventV2OrchestrationService;

        public EventV2OrchestrationServiceTests()
        {
            this.eventV2ProcessingServiceMock =
                new Mock<IEventV1ProcessingService>();

            this.eventAddressV2ProcessingServiceMock =
                new Mock<IEventAddressV1ProcessingService>();

            this.eventCallV2ProcessingServiceMock =
                new Mock<IEventCallV1ProcessingService>();

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
                new EventCallV1ProcessingValidationException(
                    someMessage,
                    someInnerException),

                new EventCallV1ProcessingDependencyValidationException(
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
                new EventCallV1ProcessingDependencyException(
                    someMessage,
                    someInnerException),

                new EventCallV1ProcessingServiceException(
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
                new EventAddressV1ProcessingValidationException(
                    someMessage,
                    someInnerException),

                new EventAddressV1ProcessingDependencyValidationException(
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
                new EventAddressV1ProcessingDependencyException(
                    someMessage,
                    someInnerException),

                new EventAddressV1ProcessingServiceException(
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
                new EventV1ProcessingValidationException(
                    someMessage,
                    someInnerException),

                new EventV1ProcessingDependencyValidationException(
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
                new EventV1ProcessingDependencyException(
                    someMessage,
                    someInnerException),

                new EventV1ProcessingServiceException(
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

        private static EventCallV1 CreateRandomEventCallV2() =>
            CreateEventCallV2Filler().Create();

        private static IQueryable<EventV1> CreateRandomEventV2s()
        {
            return CreateEventV2Filler()
                .Create(count: GetRandomNumber())
                    .AsQueryable();
        }

        private static EventAddressV1 CreateRandomEventAddressV2() =>
            CreateEventAddressV2Filler().Create();

        private static EventV1 CreateRandomEventV2() =>
            CreateEventV2Filler().Create();

        private static Filler<EventCallV1> CreateEventCallV2Filler() =>
            new Filler<EventCallV1>();

        private static Filler<EventAddressV1> CreateEventAddressV2Filler()
        {
            var filler = new Filler<EventAddressV1>();

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

        private static Filler<EventV1> CreateEventV2Filler()
        {
            var filler = new Filler<EventV1>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(GetRandomDateTimeOffset)

                .OnType<DateTimeOffset?>()
                    .Use(GetRandomDateTimeOffset());

            return filler;
        }
    }
}
