// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events
{
    public partial class EventOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldSubmitEventAsync()
        {
            // given
            Event randomEvent = CreateRandomEvent();
            Event inputEvent = randomEvent;
            Event addedEvent = inputEvent;
            Event expectedEvent = addedEvent.DeepClone();

            this.eventServiceMock.Setup(service =>
                service.AddEventAsync(inputEvent))
                    .ReturnsAsync(addedEvent);

            // when
            Event actualEvent =
                await this.eventOrchestrationService.SubmitEventAsync(
                    inputEvent);

            // then
            actualEvent.Should().BeEquivalentTo(
                expectedEvent);

            this.eventAddressServiceMock.Verify(service =>
                service.RetrieveEventAddressByIdAsync(
                    inputEvent.EventAddressId),
                        Times.Once);

            this.eventServiceMock.Verify(broker =>
                broker.AddEventAsync(inputEvent),
                    Times.Once);

            this.eventAddressServiceMock.VerifyNoOtherCalls();
            this.eventServiceMock.VerifyNoOtherCalls();
        }
    }
}
