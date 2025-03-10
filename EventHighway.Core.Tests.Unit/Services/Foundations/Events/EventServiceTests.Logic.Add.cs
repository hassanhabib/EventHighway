// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Events
{
    public partial class EventServiceTests
    {
        [Fact]
        public async Task ShouldAddEventAsync()
        {
            // given
            Event randomEvent = CreateRandomEvent();
            Event inputEvent = randomEvent;
            Event insertedEvent = inputEvent;
            Event expectedEvent = insertedEvent.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventAsync(inputEvent))
                    .ReturnsAsync(insertedEvent);

            // when
            Event actualEvent =
                await this.eventService.AddEventAsync(
                    inputEvent);

            // then
            actualEvent.Should().BeEquivalentTo(
                expectedEvent);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAsync(inputEvent),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
