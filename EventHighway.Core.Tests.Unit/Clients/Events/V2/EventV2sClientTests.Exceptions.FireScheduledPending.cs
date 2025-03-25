// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Coordinations.Events.V1.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnFireIfDependencyValidationErrorOccursAsync()
        {
            // given
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2CoordinationDependencyValidationException =
                new EventV1CoordinationDependencyValidationException(
                    someMessage,
                    someInnerException);

            var expectedEventV2ClientDependencyValidationException =
                new EventV2ClientDependencyValidationException(
                    message: "Event client validation error occurred, fix the errors and try again.",

                    innerException: eventV2CoordinationDependencyValidationException
                        .InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.FireScheduledPendingEventV1sAsync())
                    .ThrowsAsync(eventV2CoordinationDependencyValidationException);

            // when
            ValueTask fireScheduledPendingEventV2sTask =
                this.eventV2SClient.FireScheduledPendingEventV2sAsync();

            EventV2ClientDependencyValidationException actualEventV2ClientDependencyValidationException =
                await Assert.ThrowsAsync<EventV2ClientDependencyValidationException>(
                    fireScheduledPendingEventV2sTask.AsTask);

            // then
            actualEventV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2ClientDependencyValidationException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.FireScheduledPendingEventV1sAsync(),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnFireIfDependencyErrorOccursAsync()
        {
            // given
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2CoordinationDependencyException =
                new EventV1CoordinationDependencyException(
                    someMessage,
                    someInnerException);

            var expectedEventV2ClientDependencyException =
                new EventV2ClientDependencyException(
                    message: "Event client dependency error occurred, contact support.",

                    innerException: eventV2CoordinationDependencyException
                        .InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.FireScheduledPendingEventV1sAsync())
                    .ThrowsAsync(eventV2CoordinationDependencyException);

            // when
            ValueTask fireScheduledPendingEventV2sTask =
                this.eventV2SClient.FireScheduledPendingEventV2sAsync();

            EventV2ClientDependencyException actualEventV2ClientDependencyException =
                await Assert.ThrowsAsync<EventV2ClientDependencyException>(
                    fireScheduledPendingEventV2sTask.AsTask);

            // then
            actualEventV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventV2ClientDependencyException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.FireScheduledPendingEventV1sAsync(),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnFireIfServiceErrorOccursAsync()
        {
            // given
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2CoordinationServiceException =
                new EventV1CoordinationServiceException(
                    someMessage,
                    someInnerException);

            var expectedEventV2ClientServiceException =
                new EventV2ClientServiceException(
                    message: "Event client service error occurred, contact support.",

                    innerException: eventV2CoordinationServiceException
                        .InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.FireScheduledPendingEventV1sAsync())
                    .ThrowsAsync(eventV2CoordinationServiceException);

            // when
            ValueTask fireScheduledPendingEventV2sTask =
                this.eventV2SClient.FireScheduledPendingEventV2sAsync();

            EventV2ClientServiceException actualEventV2ClientServiceException =
                await Assert.ThrowsAsync<EventV2ClientServiceException>(
                    fireScheduledPendingEventV2sTask.AsTask);

            // then
            actualEventV2ClientServiceException.Should()
                .BeEquivalentTo(expectedEventV2ClientServiceException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.FireScheduledPendingEventV1sAsync(),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }
    }
}
