// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Events;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations
{
    public partial class EventCoordinationServiceTests
    {
        [Fact]
        public async Task ShouldSubmitEventAsync()
        {
            // given
            Event randomEvent = CreateRandomEvent();
            Event inputEvent = randomEvent;

            Event submittedEvent =
                inputEvent.DeepClone();

            Event submittedForListenersEvent =
                submittedEvent.DeepClone();

            Event expectedEvent =
                submittedForListenersEvent.DeepClone();

            this.eventOrchestrationServiceMock.Setup(service =>
                service.SubmitEventAsync(inputEvent))
                    .ReturnsAsync(submittedEvent);

            this.eventListenerOrchestrationServiceMock.Setup(service =>
                service.SubmitEventToListenersAsync(submittedEvent))
                    .ReturnsAsync(submittedForListenersEvent);

            // when
            Event actualEvent =
                await this.eventCoordinationService
                    .SubmitEventAsync(inputEvent);

            // then
            actualEvent.Should().BeEquivalentTo(expectedEvent);

            this.eventOrchestrationServiceMock.Verify(service =>
                service.SubmitEventAsync(inputEvent),
                    Times.Once);

            this.eventListenerOrchestrationServiceMock.Verify(service =>
                service.SubmitEventToListenersAsync(submittedEvent),
                    Times.Once);

            this.eventOrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerOrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSubmitEventAfterDelayAsync()
        {
            // given
            Event randomEvent = CreateRandomEvent();
            Event inputEvent = randomEvent.DeepClone();
            int randomSeconds = GetRandomNumber();

            TimeSpan expectedTimeDelay =
                TimeSpan.FromSeconds(randomSeconds);

            inputEvent.PublishedDate =
                DateTimeOffset.UtcNow.AddSeconds(
                    randomSeconds);

            Event submittedEvent =
                inputEvent.DeepClone();

            Event submittedForListenersEvent =
                submittedEvent.DeepClone();

            Event expectedEvent =
                submittedForListenersEvent.DeepClone();

            this.eventOrchestrationServiceMock.Setup(service =>
                service.SubmitEventAsync(inputEvent))
                    .ReturnsAsync(submittedEvent);

            this.eventListenerOrchestrationServiceMock.Setup(service =>
                service.SubmitEventToListenersAsync(submittedEvent))
                    .ReturnsAsync(submittedForListenersEvent);

            // when
            Event actualEvent =
                await this.eventCoordinationService
                    .SubmitEventAsync(inputEvent);

            await Task.Delay(expectedTimeDelay);

            // then
            actualEvent.Should().BeEquivalentTo(expectedEvent);

            this.eventOrchestrationServiceMock.Verify(service =>
                service.SubmitEventAsync(inputEvent),
                    Times.Once);

            this.eventListenerOrchestrationServiceMock.Verify(service =>
                service.SubmitEventToListenersAsync(submittedEvent),
                    Times.Once);

            this.eventOrchestrationServiceMock
                .VerifyNoOtherCalls();

            this.eventListenerOrchestrationServiceMock
                .VerifyNoOtherCalls();
        }

    }
}
