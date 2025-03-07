// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.EventCall.V2.Exceptions;
using EventHighway.Core.Services.Foundations.EventCalls.V2;
using EventHighway.Core.Services.Processings.EventCalls.V2;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventCalls.V2
{
    public partial class EventCallV2ProcessingServiceTests
    {
        private readonly Mock<IEventCallV2Service> eventCallV2ServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventCallV2ProcessingService eventCallV2ProcessingService;

        public EventCallV2ProcessingServiceTests()
        {
            this.eventCallV2ServiceMock = new Mock<IEventCallV2Service>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.eventCallV2ProcessingService = new EventCallV2ProcessingService(
                eventCallV2Service: this.eventCallV2ServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> EventCallV2ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventCallV2ValidationException(
                    someMessage,
                    someInnerException),

                new EventCallV2DependencyValidationException(
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
                new EventCallV2DependencyException(
                    someMessage,
                    someInnerException),

                new EventCallV2ServiceException(
                    someMessage,
                    someInnerException),
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static EventCallV2 CreateRandomEventCallV2() =>
            CreateEventCallV2Filler().Create();

        private static Filler<EventCallV2> CreateEventCallV2Filler() =>
            new Filler<EventCallV2>();
    }
}
