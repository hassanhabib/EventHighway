// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace EventHighway.Core.Tests.Acceptance.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        private readonly WireMockServer wireMockServer;

        private static EventListenerV2 CreateRandomEventListenerV2() =>
            CreateEventListenerV2Filler().Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static EventAddressV2 CreateRandomEventAddressV2() =>
            CreateEventAddressV2Filler().Create();

        private static Filler<EventListenerV2> CreateEventListenerV2Filler()
        {
            var filler = new Filler<EventListenerV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.EventAddress).IgnoreIt()

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.ListenerEvents).IgnoreIt();

            return filler;
        }

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
