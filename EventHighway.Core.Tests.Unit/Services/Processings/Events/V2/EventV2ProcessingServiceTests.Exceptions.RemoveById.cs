// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Processings.Events.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.Events.V2
{
    public partial class EventV2ProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnRemoveByIdIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            Guid someEventV2Id = GetRandomId();

            var expectedEventV2ProcessingDependencyValidationException =
                new EventV2ProcessingDependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventV2ServiceMock.Setup(service =>
                service.RemoveEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventV1> removeEventV2ByIdTask =
                this.eventV2ProcessingService.RemoveEventV2ByIdAsync(
                    someEventV2Id);

            EventV2ProcessingDependencyValidationException
                actualEventV2ProcessingDependencyValidationException =
                    await Assert.ThrowsAsync<EventV2ProcessingDependencyValidationException>(
                        removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2ProcessingDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2ProcessingDependencyValidationException);

            this.eventV2ServiceMock.Verify(service =>
                service.RemoveEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2ProcessingDependencyValidationException))),
                        Times.Once);

            this.eventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfDependencyExceptionOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            Guid someEventV2Id = GetRandomId();

            var expectedEventV2ProcessingDependencyException =
                new EventV2ProcessingDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption);

            this.eventV2ServiceMock.Setup(service =>
                service.RemoveEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<EventV1> removeEventV2ByIdTask =
                this.eventV2ProcessingService.RemoveEventV2ByIdAsync(
                    someEventV2Id);

            EventV2ProcessingDependencyException
                actualEventV2ProcessingDependencyException =
                    await Assert.ThrowsAsync<EventV2ProcessingDependencyException>(
                        removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2ProcessingDependencyException.Should()
                .BeEquivalentTo(expectedEventV2ProcessingDependencyException);

            this.eventV2ServiceMock.Verify(service =>
                service.RemoveEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2ProcessingDependencyException))),
                        Times.Once);

            this.eventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfExceptionOccursAndLogItAsync()
        {
            // given
            Guid someEventV2Id = GetRandomId();
            var serviceException = new Exception();

            var failedEventV2ProcessingServiceException =
                new FailedEventV2ProcessingServiceException(
                    message: "Failed event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventV2ProcessingExceptionException =
                new EventV2ProcessingServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: failedEventV2ProcessingServiceException);

            this.eventV2ServiceMock.Setup(service =>
                service.RemoveEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventV1> removeEventV2ByIdTask =
                this.eventV2ProcessingService.RemoveEventV2ByIdAsync(
                    someEventV2Id);

            EventV2ProcessingServiceException
                actualEventV2ProcessingServiceException =
                    await Assert.ThrowsAsync<EventV2ProcessingServiceException>(
                        removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2ProcessingServiceException.Should()
                .BeEquivalentTo(expectedEventV2ProcessingExceptionException);

            this.eventV2ServiceMock.Verify(service =>
                service.RemoveEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2ProcessingExceptionException))),
                        Times.Once);

            this.eventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
