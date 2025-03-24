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
        public async Task ShouldAddEventAddressV2Async()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            EventAddressV1 randomEventAddressV2 =
                CreateRandomEventAddressV2(
                    dates: randomDateTimeOffset);

            EventAddressV1 inputEventAddressV2 =
                randomEventAddressV2;

            EventAddressV1 insertedEventAddressV2 =
                inputEventAddressV2;

            EventAddressV1 expectedEventAddressV2 =
                insertedEventAddressV2.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventAddressV2Async(
                    inputEventAddressV2))
                        .ReturnsAsync(insertedEventAddressV2);

            // when
            EventAddressV1 actualEventAddressV2 =
                await this.eventAddressV2Service
                    .AddEventAddressV2Async(
                        inputEventAddressV2);

            // then
            actualEventAddressV2.Should().BeEquivalentTo(
                expectedEventAddressV2);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressV2Async(
                    inputEventAddressV2),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
