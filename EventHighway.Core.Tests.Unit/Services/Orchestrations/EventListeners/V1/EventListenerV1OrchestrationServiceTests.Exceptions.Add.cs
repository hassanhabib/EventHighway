﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V1
{
    public partial class EventListenerV1OrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(EventListenerV1ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            EventListenerV1 someEventListenerV1 = CreateRandomEventListenerV1();

            var expectedEventListenerV1OrchestrationDependencyValidationException =
                new EventListenerV1OrchestrationDependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventListenerV1ProcessingServiceMock.Setup(service =>
                service.AddEventListenerV1Async(It.IsAny<EventListenerV1>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventListenerV1> addEventListenerV1Task =
                this.eventListenerV1OrchestrationService.AddEventListenerV1Async(someEventListenerV1);

            EventListenerV1OrchestrationDependencyValidationException
                actualEventListenerV1OrchestrationDependencyValidationException =
                    await Assert.ThrowsAsync<EventListenerV1OrchestrationDependencyValidationException>(
                        addEventListenerV1Task.AsTask);

            // then
            actualEventListenerV1OrchestrationDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV1OrchestrationDependencyValidationException);

            this.eventListenerV1ProcessingServiceMock.Verify(service =>
                service.AddEventListenerV1Async(It.IsAny<EventListenerV1>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV1OrchestrationDependencyValidationException))),
                        Times.Once);

            this.eventListenerV1ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.listenerEventV1ProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(EventListenerV1DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyExceptionOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            EventListenerV1 someEventListenerV1 = CreateRandomEventListenerV1();

            var expectedEventListenerV1OrchestrationDependencyException =
                new EventListenerV1OrchestrationDependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption);

            this.eventListenerV1ProcessingServiceMock.Setup(service =>
                service.AddEventListenerV1Async(It.IsAny<EventListenerV1>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<EventListenerV1> addEventListenerV1Task =
                this.eventListenerV1OrchestrationService.AddEventListenerV1Async(someEventListenerV1);

            EventListenerV1OrchestrationDependencyException
                actualEventListenerV1OrchestrationDependencyException =
                    await Assert.ThrowsAsync<EventListenerV1OrchestrationDependencyException>(
                        addEventListenerV1Task.AsTask);

            // then
            actualEventListenerV1OrchestrationDependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV1OrchestrationDependencyException);

            this.eventListenerV1ProcessingServiceMock.Verify(service =>
                service.AddEventListenerV1Async(It.IsAny<EventListenerV1>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV1OrchestrationDependencyException))),
                        Times.Once);

            this.eventListenerV1ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.listenerEventV1ProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfExceptionOccursAndLogItAsync()
        {
            // given
            EventListenerV1 someEventListenerV1 = CreateRandomEventListenerV1();
            var serviceException = new Exception();

            var failedEventListenerV1OrchestrationServiceException =
                new FailedEventListenerV1OrchestrationServiceException(
                    message: "Failed event listener service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventListenerV1OrchestrationExceptionException =
                new EventListenerV1OrchestrationServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: failedEventListenerV1OrchestrationServiceException);

            this.eventListenerV1ProcessingServiceMock.Setup(service =>
                service.AddEventListenerV1Async(It.IsAny<EventListenerV1>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventListenerV1> addEventListenerV1Task =
                this.eventListenerV1OrchestrationService.AddEventListenerV1Async(
                    someEventListenerV1);

            EventListenerV1OrchestrationServiceException
                actualEventListenerV1OrchestrationServiceException =
                    await Assert.ThrowsAsync<EventListenerV1OrchestrationServiceException>(
                        addEventListenerV1Task.AsTask);

            // then
            actualEventListenerV1OrchestrationServiceException.Should()
                .BeEquivalentTo(expectedEventListenerV1OrchestrationExceptionException);

            this.eventListenerV1ProcessingServiceMock.Verify(service =>
                service.AddEventListenerV1Async(It.IsAny<EventListenerV1>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV1OrchestrationExceptionException))),
                        Times.Once);

            this.eventListenerV1ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.listenerEventV1ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
