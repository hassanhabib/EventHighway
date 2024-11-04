// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.EventListeners
{
    public partial class EventListenerServiceTests
    {
        [Fact]
        public async Task ShouldAddEventListenerAsync()
        {
            // given
            EventListener randomEventListener =
                CreateRandomEventListener();

            EventListener inputEventListener =
                randomEventListener;

            EventListener insertedEventListener =
                inputEventListener;

            EventListener expectedEventListener =
                insertedEventListener.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventListenerAsync(inputEventListener))
                    .ReturnsAsync(insertedEventListener);

            // when
            EventListener actualEventListener =
                await this.eventListenerService.AddEventListenerAsync(
                    inputEventListener);

            // then
            actualEventListener.Should().BeEquivalentTo(
                expectedEventListener);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerAsync(inputEventListener),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
