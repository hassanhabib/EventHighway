// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
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
        public async Task ShouldThrowDependencyValidationExceptionOnRegisterIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            EventListenerV2 someEventListenerV2 = CreateRandomEventListenerV2();

            var expectedEventListenerV2ClientDependencyValidationException =
                new EventListenerV2ClientDependencyValidationException(
                    message: "Event listener client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventListenerV2> registerEventListenerV2Task =
                this.eventListenerV2SClient.RegisterEventListenerV2Async(someEventListenerV2);

            EventListenerV2ClientDependencyValidationException
                actualEventListenerV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<EventListenerV2ClientDependencyValidationException>(
                        registerEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2ClientDependencyValidationException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRegisterIfDependencyErrorOccursAsync()
        {
            // given
            EventListenerV2 someEventListenerV2 = CreateRandomEventListenerV2();
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
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()))
                    .ThrowsAsync(eventListenerV2OrchestrationDependencyException);

            // when
            ValueTask<EventListenerV2> registerEventListenerV2Task =
                this.eventListenerV2SClient.RegisterEventListenerV2Async(someEventListenerV2);

            EventListenerV2ClientDependencyException
                actualEventListenerV2ClientDependencyException =
                    await Assert.ThrowsAsync<EventListenerV2ClientDependencyException>(
                        registerEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2ClientDependencyException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRegisterIfServiceErrorOccursAsync()
        {
            // given
            EventListenerV2 someEventListenerV2 = CreateRandomEventListenerV2();
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
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()))
                    .ThrowsAsync(eventListenerV2OrchestrationServiceException);

            // when
            ValueTask<EventListenerV2> registerEventListenerV2Task =
                this.eventListenerV2SClient.RegisterEventListenerV2Async(someEventListenerV2);

            EventListenerV2ClientServiceException actualEventListenerV2ClientServiceException =
                await Assert.ThrowsAsync<EventListenerV2ClientServiceException>(
                    registerEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ClientServiceException.Should()
                .BeEquivalentTo(expectedEventListenerV2ClientServiceException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
