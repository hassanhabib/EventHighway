// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        [Theory]
        [MemberData(nameof(EventV2ValidationExceptions))]
        [MemberData(nameof(EventListenerV2ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnSubmitIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            EventV1 someEventV2 = CreateRandomEventV2();

            var expectedEventV2CoordinationDependencyValidationException =
                new EventV2CoordinationDependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetDateTimeOffsetAsync())
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventV1> submitEventV2Task =
                this.eventV2CoordinationService.SubmitEventV2Async(someEventV2);

            EventV2CoordinationDependencyValidationException
                actualEventV2CoordinationDependencyValidationException =
                    await Assert.ThrowsAsync<EventV2CoordinationDependencyValidationException>(
                        submitEventV2Task.AsTask);

            // then
            expectedEventV2CoordinationDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2CoordinationDependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationDependencyValidationException))),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.SubmitEventV2Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV2Async(
                    It.IsAny<ListenerEventV1>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV2Async(
                    It.IsAny<ListenerEventV1>()),
                        Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(EventV2DependencyExceptions))]
        [MemberData(nameof(EventListenerV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnSubmitIfDependencyExceptionOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            EventV1 someEventV2 = CreateRandomEventV2();

            var expectedEventV2CoordinationDependencyException =
                new EventV2CoordinationDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: validationException.InnerException as Xeption);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetDateTimeOffsetAsync())
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventV1> submitEventV2Task =
                this.eventV2CoordinationService.SubmitEventV2Async(someEventV2);

            EventV2CoordinationDependencyException
                actualEventV2CoordinationDependencyException =
                    await Assert.ThrowsAsync<EventV2CoordinationDependencyException>(
                        submitEventV2Task.AsTask);

            // then
            expectedEventV2CoordinationDependencyException.Should()
                .BeEquivalentTo(expectedEventV2CoordinationDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationDependencyException))),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.SubmitEventV2Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV2Async(
                    It.IsAny<ListenerEventV1>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV2Async(
                    It.IsAny<ListenerEventV1>()),
                        Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnSubmitIfExceptionOccursAndLogItAsync()
        {
            // given
            EventV1 someEventV2 = CreateRandomEventV2();
            var serviceException = new Exception();

            var failedEventV2CoordinationServiceException =
                new FailedEventV2CoordinationServiceException(
                    message: "Failed event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventV2CoordinationServiceException =
                new EventV2CoordinationServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: failedEventV2CoordinationServiceException);

            this.dateTimeBrokerMock.Setup(service =>
                service.GetDateTimeOffsetAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventV1> submitEventV2Task =
                this.eventV2CoordinationService.SubmitEventV2Async(someEventV2);

            EventV2CoordinationServiceException
                actualEventV2CoordinationServiceException =
                    await Assert.ThrowsAsync<EventV2CoordinationServiceException>(
                        submitEventV2Task.AsTask);

            // then
            expectedEventV2CoordinationServiceException.Should()
                .BeEquivalentTo(expectedEventV2CoordinationServiceException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationServiceException))),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.SubmitEventV2Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV2Async(
                    It.IsAny<ListenerEventV1>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV2Async(
                    It.IsAny<ListenerEventV1>()),
                        Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
