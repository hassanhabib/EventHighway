// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;

namespace EventHighway.Core.Tests.Acceptance.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        [Fact]
        public async Task ShouldSubmitEventV2Async()
        {
            // given
            EventAddressV1 randomEventAddressV2 =
                await CreateRandomEventAddressV2Async();

            EventAddressV1 inputEventAddressV2 =
                randomEventAddressV2;

            Guid inputEventAddressV2Id =
                inputEventAddressV2.Id;

            int randomSeconds = GetRandomNumber();

            DateTimeOffset scheduledDate =
                DateTimeOffset.Now
                    .AddSeconds(randomSeconds);

            EventV1 randomEventV2 =
                CreateRandomEventV2(
                    inputEventAddressV2Id,
                    scheduledDate);

            EventV1 inputEventV2 = randomEventV2;
            EventV1 expectedEventV2 = inputEventV2;

            // when 
            EventV1 actualEventV2 =
                await this.clientBroker
                    .SubmitEventV2Async(inputEventV2);

            // then
            actualEventV2.Should()
                .BeEquivalentTo(expectedEventV2);

            await this.clientBroker
                .RemoveEventV2ByIdAsync(
                    actualEventV2.Id);

            await this.clientBroker
                .RemoveEventAddressV1ByIdAsync(
                    inputEventAddressV2Id);
        }
    }
}
