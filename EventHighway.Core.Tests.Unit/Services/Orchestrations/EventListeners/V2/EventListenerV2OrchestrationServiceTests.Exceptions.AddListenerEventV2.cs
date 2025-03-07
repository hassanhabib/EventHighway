// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Models.Orchestrations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(ListenerEventV2ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption listenerEventV2ValidationException)
        {
            // given
            ListenerEventV2 someListenerEventV2 = CreateRandomListenerEventV2();

            var expectedEventListenerV2OrchestrationDependencyValidationException =
                new EventListenerV2OrchestrationDependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: listenerEventV2ValidationException.InnerException as Xeption);

            this.listenerEventV2ProcessingServiceMock.Setup(service =>
                service.AddListenerEventV2Async(It.IsAny<ListenerEventV2>()))
                    .ThrowsAsync(listenerEventV2ValidationException);

            // when
            ValueTask<ListenerEventV2> addListenerEventV2Task =
                this.eventListenerV2OrchestrationService.AddListenerEventV2Async(someListenerEventV2);

            EventListenerV2OrchestrationDependencyValidationException
                actualEventListenerV2OrchestrationDependencyValidationException =
                    await Assert.ThrowsAsync<EventListenerV2OrchestrationDependencyValidationException>(
                        addListenerEventV2Task.AsTask);

            // then
            actualEventListenerV2OrchestrationDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2OrchestrationDependencyValidationException);

            this.listenerEventV2ProcessingServiceMock.Verify(service =>
                service.AddListenerEventV2Async(It.IsAny<ListenerEventV2>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2OrchestrationDependencyValidationException))),
                        Times.Once);

            this.listenerEventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
