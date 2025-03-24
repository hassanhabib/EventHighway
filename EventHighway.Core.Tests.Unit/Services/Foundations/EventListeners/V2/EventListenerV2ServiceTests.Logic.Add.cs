// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldAddEventListenerV2Async()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            EventListenerV1 randomEventListenerV2 =
                CreateRandomEventListenerV2(
                    randomDateTimeOffset);

            EventListenerV1 inputEventListenerV2 =
                randomEventListenerV2;

            EventListenerV1 storageEventListenerV2 =
                inputEventListenerV2;

            EventListenerV1 expectedEventListenerV2 =
                storageEventListenerV2.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventListenerV2Async(
                    inputEventListenerV2))
                        .ReturnsAsync(storageEventListenerV2);

            // when
            EventListenerV1 actualEventListenerV2 =
                await this.eventListenerV2Service
                    .AddEventListenerV2Async(
                        inputEventListenerV2);

            // then
            actualEventListenerV2.Should().BeEquivalentTo(
                expectedEventListenerV2);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV2Async(
                    inputEventListenerV2),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
