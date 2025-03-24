// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventAddresses.V2
{
    public partial class EventAddressV2ServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveEventAddressV2ByIdAsync()
        {
            // given
            Guid randomEventAddressV2Id = GetRandomId();
            Guid inputEventAddressV2Id = randomEventAddressV2Id;

            EventAddressV1 randomEventAddressV2 =
                CreateRandomEventAddressV2();

            EventAddressV1 selectedEventAddressV2 =
                randomEventAddressV2;

            EventAddressV1 expectedEventAddressV2 =
                selectedEventAddressV2.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventAddressV1ByIdAsync(
                    inputEventAddressV2Id))
                        .ReturnsAsync(selectedEventAddressV2);

            // when
            EventAddressV1 actualEventAddressV2 =
                await this.eventAddressV2Service
                    .RetrieveEventAddressV2ByIdAsync(
                        inputEventAddressV2Id);

            // then
            actualEventAddressV2.Should()
                .BeEquivalentTo(expectedEventAddressV2);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventAddressV1ByIdAsync(
                    inputEventAddressV2Id),
                        Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
