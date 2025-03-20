// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.EventAddresses.V2
{
    public partial class EventAddressesV2ClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRegisterIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            EventAddressV2 someEventAddressV2 = CreateRandomEventAddressV2();

            var expectedEventAddressV2ClientDependencyValidationException =
                new EventAddressV2ClientDependencyValidationException(
                    message: "Event address client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventAddressV2ServiceMock.Setup(service =>
                service.AddEventAddressV2Async(It.IsAny<EventAddressV2>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventAddressV2> registerEventAddressV2Task =
                this.eventAddressesClient.RegisterEventAddressV2Async(someEventAddressV2);

            EventAddressV2ClientDependencyValidationException
                actualEventAddressV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<EventAddressV2ClientDependencyValidationException>(
                        registerEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventAddressV2ClientDependencyValidationException);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.AddEventAddressV2Async(It.IsAny<EventAddressV2>()),
                    Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRegisterIfDependencyErrorOccursAsync()
        {
            // given
            EventAddressV2 someEventAddressV2 = CreateRandomEventAddressV2();
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
                service.AddEventAddressV2Async(It.IsAny<EventAddressV2>()))
                    .ThrowsAsync(eventV2DependencyException);

            // when
            ValueTask<EventAddressV2> registerEventAddressV2Task =
                this.eventAddressesClient.RegisterEventAddressV2Async(someEventAddressV2);

            EventAddressV2ClientDependencyException
                actualEventAddressV2ClientDependencyException =
                    await Assert.ThrowsAsync<EventAddressV2ClientDependencyException>(
                        registerEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventAddressV2ClientDependencyException);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.AddEventAddressV2Async(It.IsAny<EventAddressV2>()),
                    Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRegisterIfServiceErrorOccursAsync()
        {
            // given
            EventAddressV2 someEventAddressV2 = CreateRandomEventAddressV2();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2ServiceException =
                new EventAddressV2ServiceException(
                    someMessage,
                    someInnerException);

            var expectedEventAddressV2ClientServiceException =
                new EventAddressV2ClientServiceException(
                    message: "Event client service error occurred, contact support.",
                    innerException: eventV2ServiceException.InnerException as Xeption);

            this.eventAddressV2ServiceMock.Setup(service =>
                service.AddEventAddressV2Async(It.IsAny<EventAddressV2>()))
                    .ThrowsAsync(eventV2ServiceException);

            // when
            ValueTask<EventAddressV2> registerEventAddressV2Task =
                this.eventAddressesClient.RegisterEventAddressV2Async(someEventAddressV2);

            EventAddressV2ClientServiceException actualEventAddressV2ClientServiceException =
                await Assert.ThrowsAsync<EventAddressV2ClientServiceException>(
                    registerEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2ClientServiceException.Should()
                .BeEquivalentTo(expectedEventAddressV2ClientServiceException);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.AddEventAddressV2Async(It.IsAny<EventAddressV2>()),
                    Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
