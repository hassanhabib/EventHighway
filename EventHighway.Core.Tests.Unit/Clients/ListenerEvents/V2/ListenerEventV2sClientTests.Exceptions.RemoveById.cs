// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.ListenerEvents.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.ListenerEvents.V2
{
    public partial class ListenerEventV2sClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();

            var expectedListenerEventV2ClientDependencyValidationException =
                new ListenerEventV2ClientDependencyValidationException(
                    message: "Listener event client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveListenerEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<ListenerEventV1> removeListenerEventV2ByIdTask =
                this.listenerEventV2SClient.RemoveListenerEventV2ByIdAsync(someListenerEventV2Id);

            ListenerEventV2ClientDependencyValidationException
                actualListenerEventV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<ListenerEventV2ClientDependencyValidationException>(
                        removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ClientDependencyValidationException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfDependencyErrorOccursAsync()
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventListenerV2OrchestrationDependencyException =
                new EventListenerV2OrchestrationDependencyException(
                    someMessage,
                    someInnerException);

            var expectedListenerEventV2ClientDependencyException =
                new ListenerEventV2ClientDependencyException(
                    message: "Listener event client dependency error occurred, contact support.",

                    innerException: eventListenerV2OrchestrationDependencyException
                        .InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveListenerEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(eventListenerV2OrchestrationDependencyException);

            // when
            ValueTask<ListenerEventV1> removeListenerEventV2ByIdTask =
                this.listenerEventV2SClient.RemoveListenerEventV2ByIdAsync(someListenerEventV2Id);

            ListenerEventV2ClientDependencyException actualListenerEventV2ClientDependencyException =
                await Assert.ThrowsAsync<ListenerEventV2ClientDependencyException>(
                    removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedListenerEventV2ClientDependencyException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfServiceErrorOccursAsync()
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventListenerV2OrchestrationServiceException =
                new EventListenerV2OrchestrationServiceException(
                    someMessage,
                    someInnerException);

            var expectedListenerEventV2ClientServiceException =
                new ListenerEventV2ClientServiceException(
                    message: "Listener event client service error occurred, contact support.",

                    innerException: eventListenerV2OrchestrationServiceException
                        .InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveListenerEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(eventListenerV2OrchestrationServiceException);

            // when
            ValueTask<ListenerEventV1> removeListenerEventV2ByIdTask =
                this.listenerEventV2SClient.RemoveListenerEventV2ByIdAsync(someListenerEventV2Id);

            ListenerEventV2ClientServiceException actualListenerEventV2ClientServiceException =
                await Assert.ThrowsAsync<ListenerEventV2ClientServiceException>(
                    removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2ClientServiceException.Should()
                .BeEquivalentTo(expectedListenerEventV2ClientServiceException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
