// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents.V2
{
    public partial class ListenerEventV2ProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(ListenerEventV2ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnRemoveByIdIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption listenerEventV2ValidationException)
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();

            var expectedListenerEventV2ProcessingDependencyValidationException =
                new ListenerEventV2ProcessingDependencyValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: listenerEventV2ValidationException.InnerException as Xeption);

            this.listenerEventV2ServiceMock.Setup(service =>
                service.RemoveListenerEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(listenerEventV2ValidationException);

            // when
            ValueTask<ListenerEventV1> removeListenerEventV2ByIdTask =
                this.listenerEventV2ProcessingService.RemoveListenerEventV2ByIdAsync(
                    someListenerEventV2Id);

            ListenerEventV2ProcessingDependencyValidationException
                actualListenerEventV2ProcessingDependencyValidationException =
                    await Assert.ThrowsAsync<ListenerEventV2ProcessingDependencyValidationException>(
                        removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2ProcessingDependencyValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ProcessingDependencyValidationException);

            this.listenerEventV2ServiceMock.Verify(service =>
                service.RemoveListenerEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ProcessingDependencyValidationException))),
                        Times.Once);

            this.listenerEventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ListenerEventV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfDependencyExceptionOccursAndLogItAsync(
            Xeption listenerEventV2DependencyException)
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();

            var expectedListenerEventV2ProcessingDependencyException =
                new ListenerEventV2ProcessingDependencyException(
                    message: "Listener event dependency error occurred, contact support.",
                    innerException: listenerEventV2DependencyException.InnerException as Xeption);

            this.listenerEventV2ServiceMock.Setup(service =>
                service.RemoveListenerEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(listenerEventV2DependencyException);

            // when
            ValueTask<ListenerEventV1> removeListenerEventV2ByIdTask =
                this.listenerEventV2ProcessingService.RemoveListenerEventV2ByIdAsync(
                    someListenerEventV2Id);

            ListenerEventV2ProcessingDependencyException
                actualListenerEventV2ProcessingDependencyException =
                    await Assert.ThrowsAsync<ListenerEventV2ProcessingDependencyException>(
                        removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2ProcessingDependencyException.Should()
                .BeEquivalentTo(expectedListenerEventV2ProcessingDependencyException);

            this.listenerEventV2ServiceMock.Verify(service =>
                service.RemoveListenerEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ProcessingDependencyException))),
                        Times.Once);

            this.listenerEventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfExceptionOccursAndLogItAsync()
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();
            var serviceException = new Exception();

            var failedListenerEventV2ProcessingServiceException =
                new FailedListenerEventV2ProcessingServiceException(
                    message: "Failed listener event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedListenerEventV2ProcessingExceptionException =
                new ListenerEventV2ProcessingServiceException(
                    message: "Listener event service error occurred, contact support.",
                    innerException: failedListenerEventV2ProcessingServiceException);

            this.listenerEventV2ServiceMock.Setup(service =>
                service.RemoveListenerEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<ListenerEventV1> addListenerEventV2Task =
                this.listenerEventV2ProcessingService.RemoveListenerEventV2ByIdAsync(
                    someListenerEventV2Id);

            ListenerEventV2ProcessingServiceException
                actualListenerEventV2ProcessingServiceException =
                    await Assert.ThrowsAsync<ListenerEventV2ProcessingServiceException>(
                        addListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ProcessingServiceException.Should()
                .BeEquivalentTo(expectedListenerEventV2ProcessingExceptionException);

            this.listenerEventV2ServiceMock.Verify(service =>
                service.RemoveListenerEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ProcessingExceptionException))),
                        Times.Once);

            this.listenerEventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
