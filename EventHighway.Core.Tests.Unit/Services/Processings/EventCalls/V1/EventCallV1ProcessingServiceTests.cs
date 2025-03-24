// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions;
using EventHighway.Core.Services.Foundations.EventCalls.V1;
using EventHighway.Core.Services.Processings.EventCalls.V1;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventCalls.V1
{
    public partial class EventCallV1ProcessingServiceTests
    {
        private readonly Mock<IEventCallV1Service> eventCallV1ServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventCallV1ProcessingService eventCallV1ProcessingService;

        public EventCallV1ProcessingServiceTests()
        {
            this.eventCallV1ServiceMock = new Mock<IEventCallV1Service>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.eventCallV1ProcessingService = new EventCallV1ProcessingService(
                eventCallV1Service: this.eventCallV1ServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> EventCallV1ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventCallV1ValidationException(
                    someMessage,
                    someInnerException),

                new EventCallV1DependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }

        public static TheoryData<Xeption> EventCallV1DependencyExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventCallV1DependencyException(
                    someMessage,
                    someInnerException),

                new EventCallV1ServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static EventCallV1 CreateRandomEventCallV1() =>
            CreateEventCallV1Filler().Create();

        private static Filler<EventCallV1> CreateEventCallV1Filler() =>
            new Filler<EventCallV1>();
    }
}
