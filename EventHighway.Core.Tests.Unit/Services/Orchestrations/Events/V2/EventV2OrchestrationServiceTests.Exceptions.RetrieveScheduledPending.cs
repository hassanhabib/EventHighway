// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.Orchestrations.Events.V2.Exceptions;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(EventV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveScheduledPendingIfEventV2DependencyAndLogItAsync(
            Xeption eventV2DependencyException)
        {
            // given
            var expectedEventV2OrchestrationDependencyException =
                new EventV2OrchestrationDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: eventV2DependencyException.InnerException as Xeption);

            this.eventV2ProcessingServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV2sAsync())
                    .ThrowsAsync(eventV2DependencyException);

            // when
            ValueTask<IQueryable<EventV2>> retrieveScheduledPendingEventV2sTask =
                this.eventV2OrchestrationService.RetrieveScheduledPendingEventV2sAsync();

            EventV2OrchestrationDependencyException actualEventV2OrchestrationDependencyException =
                await Assert.ThrowsAsync<EventV2OrchestrationDependencyException>(
                    retrieveScheduledPendingEventV2sTask.AsTask);

            // then
            actualEventV2OrchestrationDependencyException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationDependencyException);

            this.eventV2ProcessingServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationDependencyException))),
                        Times.Once);

            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
