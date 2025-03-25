// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using EventHighway.Core.Clients.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions;
using EventHighway.Core.Services.Orchestrations.EventListeners.V1;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.ListenerEvents.V2
{
    public partial class ListenerEventV2sClientTests
    {
        private readonly Mock<IEventListenerV1OrchestrationService> eventListenerV2OrchestrationServiceMock;
        private readonly IListenerEventV2sClient listenerEventV2SClient;

        public ListenerEventV2sClientTests()
        {
            this.eventListenerV2OrchestrationServiceMock =
                new Mock<IEventListenerV1OrchestrationService>();

            this.listenerEventV2SClient =
                new ListenerEventV2sClient(
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

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static Guid GetRandomId() =>
            Guid.NewGuid();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static IQueryable<ListenerEventV1> CreateRandomListenerEventV2s() =>
            CreateListenerEventV2Filler().Create(count: GetRandomNumber()).AsQueryable();

        private static ListenerEventV1 CreateRandomListenerEventV2() =>
            CreateListenerEventV2Filler().Create();

        private static Filler<ListenerEventV1> CreateListenerEventV2Filler()
        {
            var filler = new Filler<ListenerEventV1>();

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
    }
}
