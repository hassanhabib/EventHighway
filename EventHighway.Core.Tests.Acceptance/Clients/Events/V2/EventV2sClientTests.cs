// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Tests.Acceptance.Brokers;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace EventHighway.Core.Tests.Acceptance.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        private readonly WireMockServer wireMockServer;
        private readonly ClientBroker clientBroker;

        public EventV2sClientTests()
        {
            this.wireMockServer = WireMockServer.Start();
            this.clientBroker = new ClientBroker();
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private async ValueTask<IQueryable<EventListenerV2>> CreateRandomEventListenerV2sAsync(
            Guid eventAddressV2Id,
            string inputMockEndpoint)
        {
            int randomNumber = GetRandomNumber();
            var randomEventListenerV2s = new List<EventListenerV2>();

            for (int index = 0; index < randomNumber; index++)
            {
                EventListenerV2 randomPostedEntitlementV2 =
                    await RegisterEventListenerV2Async(
                        eventAddressV2Id,
                        inputMockEndpoint);

                randomEventListenerV2s.Add(item: randomPostedEntitlementV2);
            }

            return randomEventListenerV2s.AsQueryable();
        }

        private async ValueTask<EventListenerV2> RegisterEventListenerV2Async(
            Guid eventAddressV2Id,
            string inputMockEndpoint)
        {
            EventListenerV2 randomEventListenerV2 =
                CreateRandomEventListenerV2(
                    eventAddressV2Id,
                    inputMockEndpoint);

            await this.clientBroker.RegisterEventListenerV2Async(
                randomEventListenerV2);

            return randomEventListenerV2;
        }

        private async ValueTask<IQueryable<EventV2>> CreateRandomEventV2sAsync(
            Guid eventAddressV2Id)
        {
            int randomNumber = GetRandomNumber();
            var randomEventV2s = new List<EventV2>();

            for (int index = 0; index < randomNumber; index++)
            {
                DateTimeOffset scheduledDate =
                    DateTimeOffset.Now.AddSeconds(seconds: 1);

                EventV2 randomPostedEntitlementV2 =
                    await SubmitEventV2Async(
                        eventAddressV2Id,
                        scheduledDate);

                randomEventV2s.Add(item: randomPostedEntitlementV2);
            }

            return randomEventV2s.AsQueryable();
        }

        private async ValueTask<EventV2> SubmitEventV2Async(
            Guid eventAddressV2Id,
            DateTimeOffset scheduledDate)
        {
            EventV2 randomEventV2 =
                CreateRandomEventV2(
                    eventAddressV2Id,
                    scheduledDate);

            await this.clientBroker.SubmitEventV2Async(
                randomEventV2);

            return randomEventV2;
        }

        private static EventListenerV2 CreateRandomEventListenerV2(
            Guid eventAddressV2Id,
            string inputMockEndpoint)
        {
            return CreateEventListenerV2Filler(
                eventAddressV2Id,
                inputMockEndpoint)
                    .Create();
        }

        private static EventV2 CreateRandomEventV2(
            Guid eventAddressV2Id,
            DateTimeOffset scheduledDate)
        {
            return CreateEventV2Filler(
                eventAddressV2Id,
                scheduledDate)
                    .Create();
        }

        private async ValueTask<EventAddressV2> CreateRandomEventAddressV2Async()
        {
            EventAddressV2 randomEventAddressV2 =
                CreateEventAddressV2Filler().Create();

            await this.clientBroker.RegisterEventAddressV2Async(
                randomEventAddressV2);

            return randomEventAddressV2;
        }

        private static Filler<EventListenerV2> CreateEventListenerV2Filler(
            Guid eventAddressV2Id,
            string inputMockEndpoint)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            var filler = new Filler<EventListenerV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(now)

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.EventAddressId).Use(eventAddressV2Id)

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.Endpoint).Use(inputMockEndpoint)

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.EventAddress).IgnoreIt()

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.ListenerEvents).IgnoreIt();

            return filler;
        }

        private static Filler<EventV2> CreateEventV2Filler(
            Guid eventAddressV2Id,
            DateTimeOffset scheduledDate)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            var filler = new Filler<EventV2>();

            filler.Setup()
                .OnProperty(eventV2 =>
                    eventV2.EventAddressId).Use(eventAddressV2Id)

                .OnProperty(eventV2 =>
                    eventV2.ScheduledDate).Use(scheduledDate)

                .OnType<DateTimeOffset>().Use(now);

            return filler;
        }

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
