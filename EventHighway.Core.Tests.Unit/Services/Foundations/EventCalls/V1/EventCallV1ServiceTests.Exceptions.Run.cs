﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventCalls.V1
{
    public partial class EventCallV1ServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRunIfCriticalDependencyErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            EventCallV1 someEventCallV1 = CreateRandomEventCallV1();

            var failedEventCallV1ConfigurationException =
                new FailedEventCallV1ConfigurationException(
                    message: "Failed event call configuration error occurred, contact support.",
                    innerException: criticalDependencyException);

            var expectedEventCallV1DependencyException =
                new EventCallV1DependencyException(
                    message: "Event call dependency error occurred, contact support.",
                    innerException: failedEventCallV1ConfigurationException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                        .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<EventCallV1> runEventCallV1Task =
                this.eventCallV1Service.RunEventCallV1Async(someEventCallV1);

            EventCallV1DependencyException actualEventCallV1DependencyException =
                await Assert.ThrowsAsync<EventCallV1DependencyException>(
                    runEventCallV1Task.AsTask);

            // then
            actualEventCallV1DependencyException.Should()
                .BeEquivalentTo(expectedEventCallV1DependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventCallV1DependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfHttpUnprocessableErrorOccursAndLogItAsync()
        {
            // given
            EventCallV1 someEventCallV1 = CreateRandomEventCallV1();
            var httpUnprocessableEntityException = new HttpResponseUnprocessableEntityException();

            var failedEventCallV1RequestException =
                new FailedEventCallV1RequestException(
                    message: "Failed event call request error occurred, fix the errors and try again.",
                    innerException: httpUnprocessableEntityException);

            var expectedEventCallV1DependencyValidationException =
                new EventCallV1DependencyValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: failedEventCallV1RequestException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                        .ThrowsAsync(httpUnprocessableEntityException);

            // when
            ValueTask<EventCallV1> runEventCallV1Task =
                this.eventCallV1Service.RunEventCallV1Async(someEventCallV1);

            EventCallV1DependencyValidationException actualEventCallV1DependencyValidationException =
                await Assert.ThrowsAsync<EventCallV1DependencyValidationException>(
                    runEventCallV1Task.AsTask);

            // then
            actualEventCallV1DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventCallV1DependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV1DependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            EventCallV1 someEventCallV1 = CreateRandomEventCallV1();

            HttpResponseBadRequestException httpBadRequestException =
                CreateHttpBadRequestException();

            var invalidEventCallV1Exception =
                new InvalidEventCallV1Exception(
                    message: "Event call is invalid, fix the errors and try again.",
                    innerException: httpBadRequestException,
                    data: httpBadRequestException.Data);

            var expectedEventCallV1DependencyValidationException =
                new EventCallV1DependencyValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: invalidEventCallV1Exception);

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                        .ThrowsAsync(httpBadRequestException);

            // when
            ValueTask<EventCallV1> runEventCallV1Task =
                this.eventCallV1Service.RunEventCallV1Async(someEventCallV1);

            EventCallV1DependencyValidationException actualEventCallV1DependencyValidationException =
                await Assert.ThrowsAsync<EventCallV1DependencyValidationException>(
                    runEventCallV1Task.AsTask);

            // then
            actualEventCallV1DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventCallV1DependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV1DependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfHttpConflictErrorOccursAndLogItAsync()
        {
            // given
            EventCallV1 someEventCallV1 = CreateRandomEventCallV1();
            var httpConflictException = new HttpResponseConflictException();

            var alreadyExistsEventCallV1Exception =
                new AlreadyExistsEventCallV1Exception(
                    message: "Event call with same id already exists, try again.",
                    innerException: httpConflictException);

            var expectedEventCallV1DependencyValidationException =
                new EventCallV1DependencyValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: alreadyExistsEventCallV1Exception);

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                        .ThrowsAsync(httpConflictException);

            // when
            ValueTask<EventCallV1> runEventCallV1Task =
                this.eventCallV1Service.RunEventCallV1Async(someEventCallV1);

            EventCallV1DependencyValidationException actualEventCallV1DependencyValidationException =
                await Assert.ThrowsAsync<EventCallV1DependencyValidationException>(
                    runEventCallV1Task.AsTask);

            // then
            actualEventCallV1DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventCallV1DependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV1DependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfFailedDependencyErrorOccursAndLogItAsync()
        {
            // given
            EventCallV1 someEventCallV1 = CreateRandomEventCallV1();

            var httpResponseFailedDependencyException =
                new HttpResponseFailedDependencyException();

            var invalidEventCallV1ReferenceException =
                new InvalidEventCallV1ReferenceException(
                    message: "Invalid event call reference error occurred, fix the errors and try again.",
                    innerException: httpResponseFailedDependencyException);

            var expectedEventCallV1DependencyValidationException =
                new EventCallV1DependencyValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: invalidEventCallV1ReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                        .ThrowsAsync(httpResponseFailedDependencyException);

            // when
            ValueTask<EventCallV1> runEventCallV1Task =
                this.eventCallV1Service.RunEventCallV1Async(someEventCallV1);

            EventCallV1DependencyValidationException actualEventCallV1DependencyValidationException =
                await Assert.ThrowsAsync<EventCallV1DependencyValidationException>(
                    runEventCallV1Task.AsTask);

            // then
            actualEventCallV1DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventCallV1DependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV1DependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfHttpErrorOccursAndLogItAsync()
        {
            // given
            EventCallV1 someEventCallV1 = CreateRandomEventCallV1();
            var httpException = new HttpResponseException();

            var failedEventCallV1DependencyException =
                new FailedEventCallV1DependencyException(
                    message: "Failed event call dependency error occurred, contact support.",
                    innerException: httpException);

            var expectedEventCallV1DependencyException =
                new EventCallV1DependencyException(
                    message: "Event call dependency error occurred, contact support.",
                    innerException: failedEventCallV1DependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                        .ThrowsAsync(httpException);

            // when
            ValueTask<EventCallV1> runEventCallV1Task =
                this.eventCallV1Service.RunEventCallV1Async(someEventCallV1);

            EventCallV1DependencyException actualEventCallV1DependencyException =
                await Assert.ThrowsAsync<EventCallV1DependencyException>(
                    runEventCallV1Task.AsTask);

            // then
            actualEventCallV1DependencyException.Should()
                .BeEquivalentTo(expectedEventCallV1DependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV1DependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfExceptionOccursAndLogItAsync()
        {
            // given
            EventCallV1 someEventCallV1 = CreateRandomEventCallV1();
            var serviceException = new Exception();

            var failedEventCallV1ServiceException =
                new FailedEventCallV1ServiceException(
                    message: "Failed event call service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventCallV1ServiceException =
                new EventCallV1ServiceException(
                    message: "Event call service error occurred, contact support.",
                    innerException: failedEventCallV1ServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                        .ThrowsAsync(serviceException);

            // when
            ValueTask<EventCallV1> runEventCallV1Task =
                this.eventCallV1Service.RunEventCallV1Async(someEventCallV1);

            EventCallV1ServiceException actualEventCallV1ServiceException =
                await Assert.ThrowsAsync<EventCallV1ServiceException>(
                    runEventCallV1Task.AsTask);

            // then
            actualEventCallV1ServiceException.Should()
                .BeEquivalentTo(expectedEventCallV1ServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV1ServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
