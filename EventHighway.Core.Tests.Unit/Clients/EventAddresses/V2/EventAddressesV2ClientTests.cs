// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Clients.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using EventHighway.Core.Services.Foundations.EventAddresses.V2;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.EventAddresses.V2
{
    public partial class EventAddressesV2ClientTests
    {
        private readonly Mock<IEventAddressV2Service> eventAddressV2ServiceMock;
        private readonly IEventAddressesV2Client eventAddressesClient;

        public EventAddressesV2ClientTests()
        {
            this.eventAddressV2ServiceMock =
                new Mock<IEventAddressV2Service>();

            this.eventAddressesClient =
                new EventAddressesV2Client(
                    eventAddressV2Service: this.eventAddressV2ServiceMock.Object);
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventAddressV2ValidationException(
                    someMessage,
                    someInnerException),

                new EventAddressV2DependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Guid GetRandomId() => 
            Guid.NewGuid();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static EventAddressV2 CreateRandomEventAddressV2() =>
            CreateEventAddressV2Filler().Create();

        private static Filler<EventAddressV2> CreateEventAddressV2Filler()
        {
            var filler = new Filler<EventAddressV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)

                .OnProperty(eventAddressV2 => eventAddressV2.Events)
                    .IgnoreIt()

                .OnProperty(eventAddressV2 => eventAddressV2.EventListeners)
                    .IgnoreIt()

                .OnProperty(eventAddressV2 => eventAddressV2.ListenerEvents)
                    .IgnoreIt();

            return filler;
        }
    }
}
