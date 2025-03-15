// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        private async Task ShouldRemoveEventListenerV2ByIdAsync()
        {
            // given
            Guid randomEventListenerV2Id = GetRandomId();
            Guid inputEventListenerV2Id = randomEventListenerV2Id;

            EventListenerV2 randomEventListenerV2 =
                CreateRandomEventListenerV2();

            EventListenerV2 retrievedEventListenerV2 =
                randomEventListenerV2;

            EventListenerV2 deletedEventListenerV2 =
                retrievedEventListenerV2;

            EventListenerV2 expectedEventListenerV2 =
                deletedEventListenerV2.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventListenerV2ByIdAsync(
                    inputEventListenerV2Id))
                        .ReturnsAsync(retrievedEventListenerV2);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteEventListenerV2Async(
                    retrievedEventListenerV2))
                        .ReturnsAsync(deletedEventListenerV2);

            // when
            EventListenerV2 actualEventListenerV2 =
                await this.eventListenerV2Service
                    .RemoveEventListenerV2ByIdAsync(
                        inputEventListenerV2Id);

            // then
            actualEventListenerV2.Should().BeEquivalentTo(
                expectedEventListenerV2);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventListenerV2ByIdAsync(
                    inputEventListenerV2Id),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteEventListenerV2Async(
                    retrievedEventListenerV2),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
