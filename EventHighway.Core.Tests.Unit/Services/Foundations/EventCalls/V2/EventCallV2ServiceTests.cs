// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Apis;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Services.Foundations.EventCalls.V2;
using Moq;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventCalls.V2
{
    public partial class EventCallV2ServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventCallV2Service eventCallV2Service;

        public EventCallV2ServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.eventCallV2Service = new EventCallV2Service(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> CriticalDependencyExceptions()
        {
            return new TheoryData<Xeption>
            {
                new HttpResponseUrlNotFoundException(),
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException(),
                new HttpResponseMethodNotAllowedException()
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static EventCallV2 CreateRandomEventCallV2() =>
            CreateEventCallV2Filler().Create();

        private static string CreateRandomResponse() =>
            new MnemonicString().GetValue();

        private static Filler<EventCallV2> CreateEventCallV2Filler() =>
            new Filler<EventCallV2>();
    }
}
