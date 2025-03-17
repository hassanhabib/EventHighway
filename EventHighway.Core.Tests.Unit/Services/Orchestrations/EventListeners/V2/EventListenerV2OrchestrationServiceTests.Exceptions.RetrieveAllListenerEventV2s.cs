// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(ListenerEventV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllListenerEventV2sIfDependencyErrorAndLogItAsync(
            Xeption listenerEventV2DependencyException)
        {
            // given
            var expectedEventListenerV2OrchestrationDependencyException =
                new EventListenerV2OrchestrationDependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: listenerEventV2DependencyException.InnerException as Xeption);

            this.listenerEventV2ProcessingServiceMock.Setup(service =>
                service.RetrieveAllListenerEventV2sAsync())
                    .ThrowsAsync(listenerEventV2DependencyException);

            // when
            ValueTask<IQueryable<ListenerEventV2>> retrieveAllListenerEventV2sTask =
                this.eventListenerV2OrchestrationService.RetrieveAllListenerEventV2sAsync();

            EventListenerV2OrchestrationDependencyException
                actualEventListenerV2OrchestrationDependencyException =
                    await Assert.ThrowsAsync<EventListenerV2OrchestrationDependencyException>(
                        retrieveAllListenerEventV2sTask.AsTask);

            // then
            actualEventListenerV2OrchestrationDependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2OrchestrationDependencyException);

            this.listenerEventV2ProcessingServiceMock.Verify(service =>
                service.RetrieveAllListenerEventV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2OrchestrationDependencyException))),
                        Times.Once);

            this.listenerEventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
