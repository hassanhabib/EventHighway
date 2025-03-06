// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Services.Foundations.ListernEvents.V2;
using EventHighway.Core.Services.Processings.ListenerEvents.V2;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents.V2
{
    public partial class ListenerEventV2ProcessingServiceTests
    {
        private readonly Mock<IListenerEventV2Service> listenerEventV2ServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IListenerEventV2ProcessingService listenerEventV2ProcessingService;

        public ListenerEventV2ProcessingServiceTests()
        {
            this.listenerEventV2ServiceMock = new Mock<IListenerEventV2Service>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.listenerEventV2ProcessingService = new ListenerEventV2ProcessingService(
                listenerEventV2Service: this.listenerEventV2ServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static ListenerEventV2 CreateRandomListenerEventV2() =>
            CreateListenerEventV2Filler().Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

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
