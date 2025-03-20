// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Clients.Events.V2;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Services.Coordinations.Events.V2;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        private readonly Mock<IEventV2CoordinationService> eventV2CoordinationServiceMock;
        private readonly IEventV2sClient eventV2SClient;

        public EventV2sClientTests()
        {
            this.eventV2CoordinationServiceMock =
                new Mock<IEventV2CoordinationService>();

            this.eventV2SClient =
                new EventV2sClient(
                    eventV2CoordinationService:
                        this.eventV2CoordinationServiceMock.Object);
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new EventV2CoordinationValidationException(
                    someMessage,
                    someInnerException),

                new EventV2CoordinationDependencyValidationException(
                    someMessage,
                    someInnerException),
            };
        }

        private static Guid GetRandomId() =>
            Guid.NewGuid();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static EventV2 CreateRandomEventV2() =>
            CreateEventV2Filler().Create();

        private static Filler<EventV2> CreateEventV2Filler()
        {
            var filler = new Filler<EventV2>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(GetRandomDateTimeOffset)

                .OnType<DateTimeOffset?>()
                    .Use(GetRandomDateTimeOffset())

                .OnProperty(eventV2 =>
                    eventV2.EventAddress).IgnoreIt()

                .OnProperty(eventV2 =>
                    eventV2.ListenerEvents).IgnoreIt();

            return filler;
        }
    }
}
