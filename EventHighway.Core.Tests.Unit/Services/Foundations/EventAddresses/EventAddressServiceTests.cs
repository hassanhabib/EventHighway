// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.EventAddresses;
using EventHighway.Core.Services.Foundations.EventAddresses;
using Moq;
using System;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.EventAddresses
{
    public partial class EventAddressServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IEventAddressService eventAddressService;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;

        public EventAddressServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.eventAddressService = new EventAddressService(
                storageBroker: this.storageBrokerMock.Object, 
                loggingBroker: this.loggingBrokerMock.Object);
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

        private static Expression<Func<Xeption, bool>> SameExceptionAs(
           Xeption expectedException)
        {
            return actualException =>
                actualException.SameExceptionAs(expectedException);
        }
    }
}