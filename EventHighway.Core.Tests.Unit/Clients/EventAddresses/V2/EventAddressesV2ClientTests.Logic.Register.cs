// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Clients.EventAddresses.V2
{
    public partial class EventAddressesV2ClientTests
    {
        [Fact]
        public async Task ShouldRegisterEventAddressV2Async()
        {
            // given
            EventAddressV1 randomEventAddressV2 =
                CreateRandomEventAddressV2();

            EventAddressV1 inputEventAddressV2 =
                randomEventAddressV2;

            EventAddressV1 registeredEventAddressV2 =
                inputEventAddressV2;

            EventAddressV1 expectedEventAddressV2 =
                registeredEventAddressV2.DeepClone();

            this.eventAddressV2ServiceMock.Setup(service =>
                service.AddEventAddressV1Async(
                    inputEventAddressV2))
                        .ReturnsAsync(registeredEventAddressV2);

            // when
            EventAddressV1 actualEventAddressV2 =
                await this.eventAddressesClient
                    .RegisterEventAddressV2Async(
                        inputEventAddressV2);

            // then
            actualEventAddressV2.Should()
                .BeEquivalentTo(expectedEventAddressV2);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.AddEventAddressV1Async(
                    inputEventAddressV2),
                        Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
