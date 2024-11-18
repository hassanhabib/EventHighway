// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.EventAddresses;
using EventHighway.Core.Services.Foundations.EventAddresses;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.EventAddresses
{
    public partial class EventAddressServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IEventAddressService eventAddressService;

        public EventAddressServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.eventAddressService = new EventAddressService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static EventAddress CreateRandomEventAddress() =>
            CreateEventAddressFiller().Create();

        private static IQueryable<EventAddress> CreateRandomEventAddresses() =>
            CreateEventAddressFiller().Create(GetRandomNumber()).AsQueryable();

        private static DateTimeOffset CreateRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Filler<EventAddress> CreateEventAddressFiller()
        {
            var filler = new Filler<EventAddress>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(CreateRandomDateTime);

            return filler;
        }
    }
}
