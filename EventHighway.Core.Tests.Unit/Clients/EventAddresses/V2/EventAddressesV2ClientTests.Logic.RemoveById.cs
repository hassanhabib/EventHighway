// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
        public async Task ShouldRemoveEventAddressV2ByIdAsync()
        {
            // given
            Guid randomEventAddressV2Id = GetRandomId();
            Guid inputEventAddressV2Id = randomEventAddressV2Id;

            EventAddressV1 randomEventAddressV2 =
                CreateRandomEventAddressV2();

            EventAddressV1 removedEventAddressV2 =
                randomEventAddressV2;

            EventAddressV1 expectedEventAddressV2 =
                removedEventAddressV2.DeepClone();

            this.eventAddressV2ServiceMock.Setup(service =>
                service.RemoveEventAddressV1ByIdAsync(
                    inputEventAddressV2Id))
                        .ReturnsAsync(removedEventAddressV2);

            // when
            EventAddressV1 actualEventAddressV2 =
                await this.eventAddressesClient
                    .RemoveEventAddressV2ByIdAsync(
                        inputEventAddressV2Id);

            // then
            actualEventAddressV2.Should()
                .BeEquivalentTo(expectedEventAddressV2);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.RemoveEventAddressV1ByIdAsync(
                    inputEventAddressV2Id),
                        Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
