// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using Moq;
using System.Threading.Tasks;
using System;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using Force.DeepCloner;
using FluentAssertions;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldAddEventListenerV2Async()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTime();

            EventListenerV2 randomEventListenerV2 =
                CreateRandomEventListenerV2(
                    randomDateTimeOffset);

            EventListenerV2 inputEventListenerV2 =
                randomEventListenerV2;

            EventListenerV2 storageEventListenerV2 =
                inputEventListenerV2;

            EventListenerV2 expectedEventListenerV2 =
                storageEventListenerV2.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventListenerV2Async(
                    inputEventListenerV2))
                        .ReturnsAsync(storageEventListenerV2);

            // when
            EventListenerV2 actualEventListenerV2 =
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
