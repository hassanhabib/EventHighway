// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Tests.Acceptance.Brokers;
using Microsoft.Data.SqlClient;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Acceptance.Clients.EventAddresses.V2
{
    public partial class EventAddressV2sClientTests
    {
        private readonly ClientBroker clientBroker;

        public EventAddressV2sClientTests() =>
            this.clientBroker = new ClientBroker();

        private static EventAddressV2 CreateRandomEventAddressV2() =>
            CreateEventAddressV2Filler().Create();

        private static Filler<EventAddressV2> CreateEventAddressV2Filler()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            var filler = new Filler<EventAddressV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(now)

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
