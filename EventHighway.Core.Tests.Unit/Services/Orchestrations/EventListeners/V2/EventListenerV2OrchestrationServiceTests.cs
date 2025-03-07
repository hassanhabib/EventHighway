// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Services.Orchestrations.EventListeners.V2;
using EventHighway.Core.Services.Processings.EventListeners.V2;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        private readonly Mock<IEventListenerV2ProcessingService> eventListenerV2ProcessingServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventListenerV2OrchestrationService eventListenerV2OrchestrationService;

        public EventListenerV2OrchestrationServiceTests()
        {
            this.eventListenerV2ProcessingServiceMock =
                new Mock<IEventListenerV2ProcessingService>();

            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.eventListenerV2OrchestrationService =
                new EventListenerV2OrchestrationService(
                    eventListenerV2ProcessingService: this.eventListenerV2ProcessingServiceMock.Object,
                    loggingBroker: this.loggingBrokerMock.Object);
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

        private static IQueryable<EventListenerV2> CreateRandomEventListenerV2s() =>
            CreateEventListenerV2Filler().Create(count: GetRandomNumber()).AsQueryable();

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
