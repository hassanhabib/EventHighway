// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.ListenerEvents.V2
{
    public partial class ListenerEventV2ServiceTests
    {
        [Fact]
        private async Task ShouldRemoveListenerEventV2ByIdAsync()
        {
            // given
            Guid randomListenerEventV2Id = GetRandomId();
            Guid inputListenerEventV2Id = randomListenerEventV2Id;

            ListenerEventV1 randomListenerEventV2 =
                CreateRandomListenerEventV2();

            ListenerEventV1 retrievedListenerEventV2 =
                randomListenerEventV2;

            ListenerEventV1 deletedListenerEventV2 =
                retrievedListenerEventV2;

            ListenerEventV1 expectedListenerEventV2 =
                deletedListenerEventV2.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectListenerEventV1ByIdAsync(
                    inputListenerEventV2Id))
                        .ReturnsAsync(retrievedListenerEventV2);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteListenerEventV1Async(
                    retrievedListenerEventV2))
                        .ReturnsAsync(deletedListenerEventV2);

            // when
            ListenerEventV1 actualListenerEventV2 =
                await this.listenerEventV2Service
                    .RemoveListenerEventV2ByIdAsync(
                        inputListenerEventV2Id);

            // then
            actualListenerEventV2.Should().BeEquivalentTo(
                expectedListenerEventV2);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV1ByIdAsync(
                    inputListenerEventV2Id),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteListenerEventV1Async(
                    retrievedListenerEventV2),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
