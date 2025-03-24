// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V2
{
    public partial class EventV2ServiceTests
    {
        [Fact]
        private async Task ShouldRemoveEventV2ByIdAsync()
        {
            // given
            Guid randomEventV2Id = GetRandomId();
            Guid inputEventV2Id = randomEventV2Id;
            EventV1 randomEventV2 = CreateRandomEventV2();
            EventV1 retrievedEventV2 = randomEventV2;
            EventV1 deletedEventV2 = retrievedEventV2;

            EventV1 expectedEventV2 =
                deletedEventV2.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventV2ByIdAsync(
                    inputEventV2Id))
                        .ReturnsAsync(retrievedEventV2);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteEventV2Async(
                    retrievedEventV2))
                        .ReturnsAsync(deletedEventV2);

            // when
            EventV1 actualEventV2 =
                await this.eventV2Service
                    .RemoveEventV2ByIdAsync(
                        inputEventV2Id);

            // then
            actualEventV2.Should().BeEquivalentTo(
                expectedEventV2);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV2ByIdAsync(
                    inputEventV2Id),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteEventV2Async(
                    retrievedEventV2),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
