// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.Orchestrations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(EventListenerV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveByEventAddressIdIfDependencyErrorOccursAndLogItAsync(
            Xeption eventListenerV2DependencyException)
        {
            // given
            Guid someEventAddressId = GetRandomId();

            var expectedEventListenerV2OrchestrationDependencyException =
                new EventListenerV2OrchestrationDependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: eventListenerV2DependencyException.InnerException as Xeption);

            this.eventListenerV2ProcessingServiceMock.Setup(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(someEventAddressId))
                    .ThrowsAsync(eventListenerV2DependencyException);

            // when
            ValueTask<IQueryable<EventListenerV2>> retrieveScheduledPendingEventListenerV2sTask =
                this.eventListenerV2OrchestrationService.RetrieveEventListenerV2sByEventAddressIdAsync(
                    someEventAddressId);

            EventListenerV2OrchestrationDependencyException actualEventListenerV2OrchestrationDependencyException =
                await Assert.ThrowsAsync<EventListenerV2OrchestrationDependencyException>(
                    retrieveScheduledPendingEventListenerV2sTask.AsTask);

            // then
            actualEventListenerV2OrchestrationDependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2OrchestrationDependencyException);

            this.eventListenerV2ProcessingServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(someEventAddressId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2OrchestrationDependencyException))),
                        Times.Once);

            this.eventListenerV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
