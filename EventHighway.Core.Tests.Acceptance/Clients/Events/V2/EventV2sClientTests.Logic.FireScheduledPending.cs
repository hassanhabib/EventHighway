﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace EventHighway.Core.Tests.Acceptance.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        [Fact]
        public async Task ShouldFireScheduledPendingEventV2sAsync()
        {
            // given
            string inputMockEndpoint = this.wireMockServer.Url;

            EventAddressV1 randomEventAddressV2 =
                await CreateRandomEventAddressV2Async();

            EventAddressV1 inputEventAddressV2 =
                randomEventAddressV2;

            Guid inputEventAddressV2Id =
                inputEventAddressV2.Id;

            IQueryable<EventV1> randomEventV2s =
                await CreateRandomEventV2sAsync(
                    inputEventAddressV2Id);

            IQueryable<EventV1> storageEventV2s =
                randomEventV2s;

            IQueryable<EventListenerV1> randomEventListenerV2s =
                await CreateRandomEventListenerV2sAsync(
                    inputEventAddressV2Id,
                    inputMockEndpoint);

            IQueryable<EventListenerV1> storageEventListenerV2s =
                randomEventListenerV2s;

            this.wireMockServer.Given(
                Request.Create()
                    .UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(code: HttpStatusCode.OK));

            // when
            await this.clientBroker.FireScheduledPendingEventV2sAsync();

            IQueryable<ListenerEventV2> storageListenerEventV2s =
                await this.clientBroker.RetrieveAllListenerEventV2s();

            // then
            foreach (ListenerEventV2 storageListenerEventV2
                in storageListenerEventV2s)
            {
                storageListenerEventV2s
                    .Where(listenerEventV2 =>
                        listenerEventV2.EventAddressId == inputEventAddressV2Id)
                            .Should().OnlyContain(listenerEventV2 =>
                                listenerEventV2.Status == ListenerEventV2Status.Success);

                await this.clientBroker.RemoveListenerEventV2ByIdAsync(
                    storageListenerEventV2.Id);
            }

            foreach (EventV1 eventV2 in storageEventV2s)
            {
                await this.clientBroker.RemoveEventV2ByIdAsync(
                    eventV2.Id);
            }

            foreach (EventListenerV1 eventListenerV2 in storageEventListenerV2s)
            {
                await this.clientBroker.RemoveEventListenerV2ByIdAsync(
                    eventListenerV2.Id);
            }

            await this.clientBroker.RemoveEventAddressV2ByIdAsync(
                inputEventAddressV2Id);
        }
    }
}
