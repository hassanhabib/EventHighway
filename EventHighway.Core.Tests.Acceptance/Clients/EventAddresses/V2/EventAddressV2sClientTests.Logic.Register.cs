// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using FluentAssertions;

namespace EventHighway.Core.Tests.Acceptance.Clients.EventAddresses.V2
{
    public partial class EventAddressV2sClientTests
    {
        [Fact]
        public async Task ShouldRegisterEventAddressV2Async()
        {
            // given
            EventAddressV2 randomEventAddressV2 =
                CreateRandomEventAddressV2();

            EventAddressV2 inputEventAddressV2 =
                randomEventAddressV2;

            EventAddressV2 expectedEventAddressV2 =
                inputEventAddressV2;

            // when
            EventAddressV2 actualEventAddressV2 =
                await this.clientBroker
                    .RegisterEventAddressV2Async(
                        inputEventAddressV2);

            // then
            actualEventAddressV2.Should()
                .BeEquivalentTo(expectedEventAddressV2);

            await this.clientBroker
                .RemoveEventAddressV2ByIdAsync(
                    actualEventAddressV2.Id);
        }
    }
}
