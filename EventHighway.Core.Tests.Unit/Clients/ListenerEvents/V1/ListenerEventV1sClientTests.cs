// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using EventHighway.Core.Clients.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions;
using EventHighway.Core.Services.Orchestrations.EventListeners.V1;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.ListenerEvents.V1
{
    public partial class ListenerEventV1sClientTests
    {
        private readonly Mock<IEventListenerV1OrchestrationService> eventListenerV1OrchestrationServiceMock;
        private readonly IListenerEventV1sClient listenerEventV1SClient;

        public ListenerEventV1sClientTests()
        {
            this.eventListenerV1OrchestrationServiceMock =
                new Mock<IEventListenerV1OrchestrationService>();

            this.listenerEventV1SClient =
                new ListenerEventV1sClient(
                    eventListenerV1OrchestrationService:
                        this.eventListenerV1OrchestrationServiceMock.Object);
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

        private static IQueryable<ListenerEventV1> CreateRandomListenerEventV1s() =>
            CreateListenerEventV1Filler().Create(count: GetRandomNumber()).AsQueryable();

        private static ListenerEventV1 CreateRandomListenerEventV1() =>
            CreateListenerEventV1Filler().Create();

        private static Filler<ListenerEventV1> CreateListenerEventV1Filler()
        {
            var filler = new Filler<ListenerEventV1>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)

                .OnProperty(listenerEventV1 => listenerEventV1.Event)
                    .IgnoreIt()

                .OnProperty(listenerEventV1 => listenerEventV1.EventAddress)
                    .IgnoreIt()

                .OnProperty(listenerEventV1 => listenerEventV1.EventListener)
                    .IgnoreIt();

            return filler;
        }
    }
}
