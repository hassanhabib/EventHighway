// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        [Theory]
        [MemberData(nameof(EventV2ValidationExceptions))]
        [MemberData(nameof(EventListenerV2ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnFireScheduledPendingIfDependencyValidationAndLogItAsync(
            Xeption validationException)
        {
            // given
            var expectedEventV2CoordinationDependencyValidationException =
                new EventV2CoordinationDependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV2sAsync())
                    .ThrowsAsync(validationException);

            // when
            ValueTask fireScheduledPendingEventV2sTask =
                this.eventV2CoordinationService.FireScheduledPendingEventV2sAsync();

            EventV2CoordinationDependencyValidationException
                actualEventV2CoordinationDependencyValidationException =
                    await Assert.ThrowsAsync<EventV2CoordinationDependencyValidationException>(
                        fireScheduledPendingEventV2sTask.AsTask);

            // then
            expectedEventV2CoordinationDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2CoordinationDependencyValidationException);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationDependencyValidationException))),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(EventV2DependencyExceptions))]
        [MemberData(nameof(EventListenerV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnFireScheduledPendingIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            var expectedEventV2CoordinationDependencyException =
                new EventV2CoordinationDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption);

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV2sAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask fireScheduledPendingEventV2sTask =
                this.eventV2CoordinationService.FireScheduledPendingEventV2sAsync();

            EventV2CoordinationDependencyException actualEventV2CoordinationDependencyException =
                await Assert.ThrowsAsync<EventV2CoordinationDependencyException>(
                    fireScheduledPendingEventV2sTask.AsTask);

            // then
            actualEventV2CoordinationDependencyException.Should()
                .BeEquivalentTo(expectedEventV2CoordinationDependencyException);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationDependencyException))),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnFireScheduledPendingIfExceptionOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedEventV2CoordinationServiceException =
                new FailedEventV2CoordinationServiceException(
                    message: "Failed event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventV2CoordinationServiceException =
                new EventV2CoordinationServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: failedEventV2CoordinationServiceException);

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV2sAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask fireScheduledPendingEventV2sTask =
                this.eventV2CoordinationService.FireScheduledPendingEventV2sAsync();

            EventV2CoordinationServiceException actualEventV2CoordinationServiceException =
                await Assert.ThrowsAsync<EventV2CoordinationServiceException>(
                    fireScheduledPendingEventV2sTask.AsTask);

            // then
            actualEventV2CoordinationServiceException.Should()
                .BeEquivalentTo(expectedEventV2CoordinationServiceException);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationServiceException))),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
