// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using EventHighway.Core.Clients.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Services.Orchestrations.EventListeners.V2;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Clients.ListenerEvents.V2
{
    public partial class ListenerEventV2sClientTests
    {
        private readonly Mock<IEventListenerV2OrchestrationService> eventListenerV2OrchestrationServiceMock;
        private readonly IListenerEventV2sClient listenerEventV2SClient;

        public ListenerEventV2sClientTests()
        {
            this.eventListenerV2OrchestrationServiceMock =
                new Mock<IEventListenerV2OrchestrationService>();

            this.listenerEventV2SClient =
                new ListenerEventV2sClient(
                    eventListenerV2OrchestrationService:
                        this.eventListenerV2OrchestrationServiceMock.Object);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static IQueryable<ListenerEventV2> CreateRandomListenerEventV2s() =>
            CreateListenerEventV2Filler().Create(count: GetRandomNumber()).AsQueryable();

        private static Filler<ListenerEventV2> CreateListenerEventV2Filler()
        {
            var filler = new Filler<ListenerEventV2>();

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
