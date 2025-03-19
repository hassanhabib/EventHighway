// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfEventListenerV2IsNullAndLogItAsync()
        {
            // given
            EventListenerV2 nullEventListenerV2 = null;

            var nullEventListenerV2OrchestrationException =
                new NullEventListenerV2OrchestrationException(message: "Event listener is null.");

            var expectedEventListenerV2OrchestrationValidationException =
                new EventListenerV2OrchestrationValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: nullEventListenerV2OrchestrationException);

            // when
            ValueTask<EventListenerV2> addEventListenerV2Task =
                this.eventListenerV2OrchestrationService.AddEventListenerV2Async(nullEventListenerV2);

            EventListenerV2OrchestrationValidationException
                actualEventListenerV2OrchestrationValidationException =
                    await Assert.ThrowsAsync<EventListenerV2OrchestrationValidationException>(
                        addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2OrchestrationValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2OrchestrationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2OrchestrationValidationException))),
                        Times.Once);

            this.eventListenerV2ProcessingServiceMock.Verify(broker =>
                broker.AddEventListenerV2Async(
                    It.IsAny<EventListenerV2>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.listenerEventV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
