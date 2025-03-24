// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.EventAddresses.V2
{
    public partial class EventAddressesV2ClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            Guid someEventAddressV2Id = GetRandomId();

            var expectedEventAddressV2ClientDependencyValidationException =
                new EventAddressV2ClientDependencyValidationException(
                    message: "Event address client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventAddressV2ServiceMock.Setup(service =>
                service.RemoveEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventAddressV1> removeEventAddressV2ByIdTask =
                this.eventAddressesClient.RemoveEventAddressV2ByIdAsync(someEventAddressV2Id);

            EventAddressV2ClientDependencyValidationException
                actualEventAddressV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<EventAddressV2ClientDependencyValidationException>(
                        removeEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventAddressV2ClientDependencyValidationException);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.RemoveEventAddressV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveByIdIfDependencyErrorOccursAsync()
        {
            // given
            Guid someEventAddressV2Id = GetRandomId();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2DependencyException =
                new EventAddressV2DependencyException(
                    someMessage,
                    someInnerException);

            var expectedEventAddressV2ClientDependencyException =
                new EventAddressV2ClientDependencyException(
                    message: "Event address client dependency error occurred, contact support.",
                    innerException: eventV2DependencyException.InnerException as Xeption);

            this.eventAddressV2ServiceMock.Setup(service =>
                service.RemoveEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(eventV2DependencyException);

            // when
            ValueTask<EventAddressV1> removeEventAddressV2ByIdTask =
                this.eventAddressesClient.RemoveEventAddressV2ByIdAsync(someEventAddressV2Id);

            EventAddressV2ClientDependencyException
                actualEventAddressV2ClientDependencyException =
                    await Assert.ThrowsAsync<EventAddressV2ClientDependencyException>(
                        removeEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventAddressV2ClientDependencyException);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.RemoveEventAddressV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfServiceErrorOccursAsync()
        {
            // given
            Guid someEventAddressV2Id = GetRandomId();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2ServiceException =
                new EventAddressV2ServiceException(
                    someMessage,
                    someInnerException);

            var expectedEventAddressV2ClientServiceException =
                new EventAddressV2ClientServiceException(
                    message: "Event address client service error occurred, contact support.",
                    innerException: eventV2ServiceException.InnerException as Xeption);

            this.eventAddressV2ServiceMock.Setup(service =>
                service.RemoveEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(eventV2ServiceException);

            // when
            ValueTask<EventAddressV1> removeEventAddressV2ByIdTask =
                this.eventAddressesClient.RemoveEventAddressV2ByIdAsync(someEventAddressV2Id);

            EventAddressV2ClientServiceException actualEventAddressV2ClientServiceException =
                await Assert.ThrowsAsync<EventAddressV2ClientServiceException>(
                    removeEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2ClientServiceException.Should()
                .BeEquivalentTo(expectedEventAddressV2ClientServiceException);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.RemoveEventAddressV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
