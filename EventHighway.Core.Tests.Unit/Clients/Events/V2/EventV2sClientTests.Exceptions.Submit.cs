﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnSubmitIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            EventV2 someEventV2 = CreateRandomEventV2();

            var expectedEventV2ClientDependencyValidationException =
                new EventV2ClientDependencyValidationException(
                    message: "Event client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventV2> submitEventV2Task =
                this.eventV2SClient.SubmitEventV2Async(someEventV2);

            EventV2ClientDependencyValidationException actualEventV2ClientDependencyValidationException =
                await Assert.ThrowsAsync<EventV2ClientDependencyValidationException>(
                    submitEventV2Task.AsTask);

            // then
            actualEventV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2ClientDependencyValidationException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnSubmitIfDependencyErrorOccursAsync()
        {
            // given
            EventV2 someEventV2 = CreateRandomEventV2();
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
                service.SubmitEventV2Async(It.IsAny<EventV2>()))
                    .ThrowsAsync(eventV2CoordinationDependencyException);

            // when
            ValueTask<EventV2> submitEventV2Task =
                this.eventV2SClient.SubmitEventV2Async(someEventV2);

            EventV2ClientDependencyException actualEventV2ClientDependencyException =
                await Assert.ThrowsAsync<EventV2ClientDependencyException>(
                    submitEventV2Task.AsTask);

            // then
            actualEventV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventV2ClientDependencyException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnSubmitIfServiceErrorOccursAsync()
        {
            // given
            EventV2 someEventV2 = CreateRandomEventV2();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2CoordinationServiceException =
                new EventV2CoordinationServiceException(
                    someMessage,
                    someInnerException);

            var expectedEventV2ClientServiceException =
                new EventV2ClientServiceException(
                    message: "Event client service error occurred, contact support.",

                    innerException: eventV2CoordinationServiceException
                        .InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()))
                    .ThrowsAsync(eventV2CoordinationServiceException);

            // when
            ValueTask<EventV2> submitEventV2Task =
                this.eventV2SClient.SubmitEventV2Async(someEventV2);

            EventV2ClientServiceException actualEventV2ClientServiceException =
                await Assert.ThrowsAsync<EventV2ClientServiceException>(
                    submitEventV2Task.AsTask);

            // then
            actualEventV2ClientServiceException.Should()
                .BeEquivalentTo(expectedEventV2ClientServiceException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }
    }
}
