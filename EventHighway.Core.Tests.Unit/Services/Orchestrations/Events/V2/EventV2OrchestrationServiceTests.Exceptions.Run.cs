// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(EventCallV2ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRunIfEventCallV2ValidationErrorOccursAndLogItAsync(
            Xeption eventCallV2ValidationException)
        {
            // given
            EventCallV1 someEventCallV2 = CreateRandomEventCallV2();

            var expectedEventV2OrchestrationDependencyValidationException =
                new EventV2OrchestrationDependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: eventCallV2ValidationException.InnerException as Xeption);

            this.eventCallV2ProcessingServiceMock.Setup(service =>
                service.RunEventCallV1Async(It.IsAny<EventCallV1>()))
                    .ThrowsAsync(eventCallV2ValidationException);

            // when
            ValueTask<EventCallV1> runEventCallV2Task =
                this.eventV2OrchestrationService.RunEventCallV2Async(
                    someEventCallV2);

            EventV2OrchestrationDependencyValidationException
                actualEventV2OrchestrationDependencyValidationException =
                    await Assert.ThrowsAsync<EventV2OrchestrationDependencyValidationException>(
                        runEventCallV2Task.AsTask);

            // then
            actualEventV2OrchestrationDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationDependencyValidationException);

            this.eventCallV2ProcessingServiceMock.Verify(service =>
                service.RunEventCallV1Async(It.IsAny<EventCallV1>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationDependencyValidationException))),
                        Times.Once);

            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(EventCallV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRunIfEventCallV2DependencyErrorOccursAndLogItAsync(
            Xeption eventCallV2DependencyException)
        {
            // given
            EventCallV1 someEventCallV2 = CreateRandomEventCallV2();

            var expectedEventV2OrchestrationDependencyException =
                new EventV2OrchestrationDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: eventCallV2DependencyException.InnerException as Xeption);

            this.eventCallV2ProcessingServiceMock.Setup(service =>
                service.RunEventCallV1Async(It.IsAny<EventCallV1>()))
                    .ThrowsAsync(eventCallV2DependencyException);

            // when
            ValueTask<EventCallV1> runEventCallV2Task =
                this.eventV2OrchestrationService.RunEventCallV2Async(
                    someEventCallV2);

            EventV2OrchestrationDependencyException actualEventV2OrchestrationDependencyException =
                await Assert.ThrowsAsync<EventV2OrchestrationDependencyException>(
                    runEventCallV2Task.AsTask);

            // then
            actualEventV2OrchestrationDependencyException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationDependencyException);

            this.eventCallV2ProcessingServiceMock.Verify(service =>
                service.RunEventCallV1Async(It.IsAny<EventCallV1>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationDependencyException))),
                        Times.Once);

            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRunIfExceptionOccursAndLogItAsync()
        {
            // given
            EventCallV1 someEventCallV2 = CreateRandomEventCallV2();
            var serviceException = new Exception();

            var failedEventV2OrchestrationServiceException =
                new FailedEventV2OrchestrationServiceException(
                    message: "Failed event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventV2OrchestrationServiceException =
                new EventV2OrchestrationServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: failedEventV2OrchestrationServiceException);

            this.eventCallV2ProcessingServiceMock.Setup(service =>
                service.RunEventCallV1Async(It.IsAny<EventCallV1>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventCallV1> runEventCallV2Task =
                this.eventV2OrchestrationService.RunEventCallV2Async(
                    someEventCallV2);

            EventV2OrchestrationServiceException actualEventV2OrchestrationServiceException =
                await Assert.ThrowsAsync<EventV2OrchestrationServiceException>(
                    runEventCallV2Task.AsTask);

            // then
            actualEventV2OrchestrationServiceException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationServiceException);

            this.eventCallV2ProcessingServiceMock.Verify(service =>
                service.RunEventCallV1Async(It.IsAny<EventCallV1>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationServiceException))),
                        Times.Once);

            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
