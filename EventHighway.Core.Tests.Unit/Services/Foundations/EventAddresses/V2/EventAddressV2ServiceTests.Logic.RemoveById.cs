// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventAddresses.V2
{
    public partial class EventAddressV2ServiceTests
    {
        [Fact]
        private async Task ShouldRemoveEventAddressV2ByIdAsync()
        {
            // given
            Guid randomEventAddressV2Id = GetRandomId();
            Guid inputEventAddressV2Id = randomEventAddressV2Id;

            EventAddressV2 randomEventAddressV2 =
                CreateRandomEventAddressV2();

            EventAddressV2 retrievedEventAddressV2 =
                randomEventAddressV2;

            EventAddressV2 deletedEventAddressV2 =
                retrievedEventAddressV2;

            EventAddressV2 expectedEventAddressV2 =
                deletedEventAddressV2.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventAddressV2ByIdAsync(
                    inputEventAddressV2Id))
                        .ReturnsAsync(retrievedEventAddressV2);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteEventAddressV2Async(
                    retrievedEventAddressV2))
                        .ReturnsAsync(deletedEventAddressV2);

            // when
            EventAddressV2 actualEventAddressV2 =
                await this.eventAddressV2Service
                    .RemoveEventAddressV2ByIdAsync(
                        inputEventAddressV2Id);

            // then
            actualEventAddressV2.Should().BeEquivalentTo(
                expectedEventAddressV2);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventAddressV2ByIdAsync(
                    inputEventAddressV2Id),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteEventAddressV2Async(
                    retrievedEventAddressV2),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
