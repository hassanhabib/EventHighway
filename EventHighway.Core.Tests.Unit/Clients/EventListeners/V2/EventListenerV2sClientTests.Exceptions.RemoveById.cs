// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.EventListeners.V2
{
    public partial class EventListenerV2sClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            Guid someEventListenerV2Id = GetRandomId();

            var expectedEventListenerV2ClientDependencyValidationException =
                new EventListenerV2ClientDependencyValidationException(
                    message: "Event listener client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveEventListenerV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventListenerV1> removeEventListenerV2ByIdTask =
                this.eventListenerV2SClient.RemoveEventListenerV2ByIdAsync(someEventListenerV2Id);

            EventListenerV2ClientDependencyValidationException
                actualEventListenerV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<EventListenerV2ClientDependencyValidationException>(
                        removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2ClientDependencyValidationException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveEventListenerV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfDependencyErrorOccursAsync()
        {
            // given
            Guid someEventListenerV2Id = GetRandomId();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventListenerV2OrchestrationDependencyException =
                new EventListenerV2OrchestrationDependencyException(
                    someMessage,
                    someInnerException);

            var expectedEventListenerV2ClientDependencyException =
                new EventListenerV2ClientDependencyException(
                    message: "Event listener client dependency error occurred, contact support.",

                    innerException: eventListenerV2OrchestrationDependencyException
                        .InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveEventListenerV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(eventListenerV2OrchestrationDependencyException);

            // when
            ValueTask<EventListenerV1> removeEventListenerV2ByIdTask =
                this.eventListenerV2SClient.RemoveEventListenerV2ByIdAsync(someEventListenerV2Id);

            EventListenerV2ClientDependencyException
                actualEventListenerV2ClientDependencyException =
                    await Assert.ThrowsAsync<EventListenerV2ClientDependencyException>(
                        removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2ClientDependencyException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveEventListenerV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfServiceErrorOccursAsync()
        {
            // given
            Guid someEventListenerV2Id = GetRandomId();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventListenerV2OrchestrationServiceException =
                new EventListenerV2OrchestrationServiceException(
                    someMessage,
                    someInnerException);

            var expectedEventListenerV2ClientServiceException =
                new EventListenerV2ClientServiceException(
                    message: "Event listener client service error occurred, contact support.",

                    innerException: eventListenerV2OrchestrationServiceException
                        .InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveEventListenerV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(eventListenerV2OrchestrationServiceException);

            // when
            ValueTask<EventListenerV1> removeEventListenerV2ByIdTask =
                this.eventListenerV2SClient.RemoveEventListenerV2ByIdAsync(someEventListenerV2Id);

            EventListenerV2ClientServiceException actualEventListenerV2ClientServiceException =
                await Assert.ThrowsAsync<EventListenerV2ClientServiceException>(
                    removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2ClientServiceException.Should()
                .BeEquivalentTo(expectedEventListenerV2ClientServiceException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveEventListenerV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
