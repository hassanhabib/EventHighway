// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Services.Foundations.ListernEvents.V2;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.ListenerEvents.V2
{
    public partial class ListenerEventV2ServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IListenerEventV2Service listenerEventV2Service;

        public ListenerEventV2ServiceTests()
        {
            this.storageBrokerMock =
                new Mock<IStorageBroker>();

            this.loggingBrokerMock =
                new Mock<ILoggingBroker>();

            this.dateTimeBrokerMock =
                new Mock<IDateTimeBroker>();

            this.listenerEventV2Service = new ListenerEventV2Service(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
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
