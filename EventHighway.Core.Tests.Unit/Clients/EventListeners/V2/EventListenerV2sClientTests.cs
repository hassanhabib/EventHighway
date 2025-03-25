// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Clients.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions;
using EventHighway.Core.Services.Orchestrations.EventListeners.V1;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.EventListeners.V2
{
    public partial class EventListenerV2sClientTests
    {
        private readonly Mock<IEventListenerV1OrchestrationService> eventListenerV2OrchestrationServiceMock;
        private readonly IEventListenerV2sClient eventListenerV2SClient;

        public EventListenerV2sClientTests()
        {
            this.eventListenerV2OrchestrationServiceMock =
                new Mock<IEventListenerV1OrchestrationService>();

            this.eventListenerV2SClient =
                new EventListenerV2sClient(
                    eventListenerV2OrchestrationService:
                        this.eventListenerV2OrchestrationServiceMock.Object);
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventListenerV1OrchestrationValidationException(
                    someMessage,
                    someInnerException),

                new EventListenerV1OrchestrationDependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Guid GetRandomId() =>
            Guid.NewGuid();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static EventListenerV1 CreateRandomEventListenerV2() =>
            CreateEventListenerV2Filler().Create();

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
