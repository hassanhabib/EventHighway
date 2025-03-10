// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
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
        public async Task ShouldThrowValidationOnRetrieveByEventAddressIdIfEventAddressIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventAddressId = Guid.Empty;

            var invalidEventListenerV2OrchestrationException =
                new InvalidEventListenerV2OrchestrationException(
                    message: "Event listener is invalid, fix the errors and try again.");

            invalidEventListenerV2OrchestrationException.AddData(
                key: nameof(EventListenerV2.EventAddressId),
                values: "Required");

            var expectedEventListenerV2OrchestrationValidationException =
                new EventListenerV2OrchestrationValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: invalidEventListenerV2OrchestrationException);

            // when
            ValueTask<IQueryable<EventListenerV2>> retrieveEventListenerV2sByEventAddressIdTask =
                this.eventListenerV2OrchestrationService.RetrieveEventListenerV2sByEventAddressIdAsync(
                    invalidEventAddressId);

            EventListenerV2OrchestrationValidationException actualEventListenerV2OrchestrationValidationException =
                await Assert.ThrowsAsync<EventListenerV2OrchestrationValidationException>(
                    retrieveEventListenerV2sByEventAddressIdTask.AsTask);

            // then
            actualEventListenerV2OrchestrationValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2OrchestrationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2OrchestrationValidationException))),
                        Times.Once);

            this.eventListenerV2ProcessingServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.listenerEventV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
