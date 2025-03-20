// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfDependencyValidationErrorOccursAsync()
        {
            // given
            Guid someEventV2Id = GetRandomId();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2CoordinationDependencyValidationException =
                new EventV2CoordinationDependencyValidationException(
                    someMessage,
                    someInnerException);

            var expectedEventV2ClientDependencyValidationException =
                new EventV2ClientDependencyValidationException(
                    message: "Event client validation error occurred, fix the errors and try again.",

                    innerException: eventV2CoordinationDependencyValidationException
                        .InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.RemoveEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(eventV2CoordinationDependencyValidationException);

            // when
            ValueTask<EventV2> removeEventV2ByIdTask =
                this.eventV2SClient.RemoveEventV2ByIdAsync(someEventV2Id);

            EventV2ClientDependencyValidationException actualEventV2ClientDependencyValidationException =
                await Assert.ThrowsAsync<EventV2ClientDependencyValidationException>(
                    removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2ClientDependencyValidationException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.RemoveEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfDependencyErrorOccursAsync()
        {
            // given
            Guid someEventV2Id = GetRandomId();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2CoordinationDependencyException =
                new EventV2CoordinationDependencyException(
                    someMessage,
                    someInnerException);

            var expectedEventV2ClientDependencyException =
                new EventV2ClientDependencyException(
                    message: "Event client dependency error occurred, contact support.",

                    innerException: eventV2CoordinationDependencyException
                        .InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.RemoveEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(eventV2CoordinationDependencyException);

            // when
            ValueTask<EventV2> removeEventV2ByIdTask =
                this.eventV2SClient.RemoveEventV2ByIdAsync(someEventV2Id);

            EventV2ClientDependencyException actualEventV2ClientDependencyException =
                await Assert.ThrowsAsync<EventV2ClientDependencyException>(
                    removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventV2ClientDependencyException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.RemoveEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }
    }
}
