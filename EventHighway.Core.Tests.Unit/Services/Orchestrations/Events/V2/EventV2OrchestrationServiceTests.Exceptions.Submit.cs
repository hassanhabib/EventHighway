// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(EventV2ValidationExceptions))]
        [MemberData(nameof(EventAddressV2ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnSubmitIfValidationExceptionOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            EventV1 someEventV2 = CreateRandomEventV2();

            var expectedEventV2OrchestrationDependencyValidationException =
                new EventV2OrchestrationDependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventAddressV2ProcessingServiceMock.Setup(service =>
                service.RetrieveEventAddressV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventV1> submitEventV2Task =
                this.eventV2OrchestrationService.SubmitEventV2Async(
                    someEventV2);

            EventV2OrchestrationDependencyValidationException
                actualEventV2OrchestrationDependencyValidationException =
                    await Assert.ThrowsAsync<EventV2OrchestrationDependencyValidationException>(
                        submitEventV2Task.AsTask);

            // then
            actualEventV2OrchestrationDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationDependencyValidationException);

            this.eventAddressV2ProcessingServiceMock.Verify(service =>
                service.RetrieveEventAddressV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationDependencyValidationException))),
                        Times.Once);

            this.eventV2ProcessingServiceMock.Verify(broker =>
                broker.AddEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(EventV2DependencyExceptions))]
        [MemberData(nameof(EventAddressV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnSubmitIfDependencyExceptionOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            EventV1 someEventV2 = CreateRandomEventV2();

            var expectedEventV2OrchestrationDependencyException =
                new EventV2OrchestrationDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption);

            this.eventAddressV2ProcessingServiceMock.Setup(service =>
                service.RetrieveEventAddressV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<EventV1> submitEventV2Task =
                this.eventV2OrchestrationService.SubmitEventV2Async(
                    someEventV2);

            EventV2OrchestrationDependencyException
                actualEventV2OrchestrationDependencyException =
                    await Assert.ThrowsAsync<EventV2OrchestrationDependencyException>(
                        submitEventV2Task.AsTask);

            // then
            actualEventV2OrchestrationDependencyException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationDependencyException);

            this.eventAddressV2ProcessingServiceMock.Verify(service =>
                service.RetrieveEventAddressV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationDependencyException))),
                        Times.Once);

            this.eventV2ProcessingServiceMock.Verify(broker =>
                broker.AddEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnSubmitIfExceptionOccursAndLogItAsync()
        {
            // given
            EventV1 someEventV2 = CreateRandomEventV2();
            var serviceException = new Exception();

            var failedEventV2OrchestrationServiceException =
                new FailedEventV2OrchestrationServiceException(
                    message: "Failed event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventV2OrchestrationServiceException =
                new EventV2OrchestrationServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: failedEventV2OrchestrationServiceException);

            this.eventAddressV2ProcessingServiceMock.Setup(service =>
                service.RetrieveEventAddressV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventV1> submitEventV2Task =
                this.eventV2OrchestrationService.SubmitEventV2Async(
                    someEventV2);

            EventV2OrchestrationServiceException
                actualEventV2OrchestrationServiceException =
                    await Assert.ThrowsAsync<EventV2OrchestrationServiceException>(
                        submitEventV2Task.AsTask);

            // then
            actualEventV2OrchestrationServiceException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationServiceException);

            this.eventAddressV2ProcessingServiceMock.Verify(service =>
                service.RetrieveEventAddressV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationServiceException))),
                        Times.Once);

            this.eventV2ProcessingServiceMock.Verify(broker =>
                broker.AddEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
