// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.EventAddresses;
using EventHighway.Core.Services.EventAddresses;
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

        private static DateTimeOffset CreateRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<EventAddress> CreateEventAddressFiller()
        {
            var filler = new Filler<EventAddress>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(CreateRandomDateTime);
            
            return filler;
        }
    }
}
