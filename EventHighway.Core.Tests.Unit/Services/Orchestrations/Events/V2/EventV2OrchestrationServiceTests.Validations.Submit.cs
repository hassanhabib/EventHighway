// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnSubmitIfEventV2IsNullAndLogItAsync()
        {
            // given
            EventV1 nullEventV2 = null;

            var nullEventV2OrchestrationException =
                new NullEventV2OrchestrationException(message: "Event is null.");

            var expectedEventV2OrchestrationValidationException =
                new EventV2OrchestrationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: nullEventV2OrchestrationException);

            // when
            ValueTask<EventV1> submitEventV2Task =
                this.eventV2OrchestrationService.SubmitEventV2Async(nullEventV2);

            EventV2OrchestrationValidationException
                actualEventV2OrchestrationValidationException =
                    await Assert.ThrowsAsync<EventV2OrchestrationValidationException>(
                        submitEventV2Task.AsTask);

            // then
            actualEventV2OrchestrationValidationException.Should().BeEquivalentTo(
                expectedEventV2OrchestrationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationValidationException))),
                        Times.Once);

            this.eventAddressV2ProcessingServiceMock.Verify(service =>
                service.RetrieveEventAddressV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.eventV2ProcessingServiceMock.Verify(broker =>
                broker.AddEventV2Async(
                    It.IsAny<EventV1>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfEventAddressV2IsNotFoundAndLogItAsync()
        {
            // given
            EventV1 someEventV2 = CreateRandomEventV2();
            Guid nonExistingEventAddressV2Id = someEventV2.EventAddressId;
            EventAddressV2 nullEventAddressV2 = null;

            var notFoundEventAddressV2OrchestrationException =
                new NotFoundEventAddressV2OrchestrationException(
                    message: $"Could not find event address with id: {nonExistingEventAddressV2Id}.");

            var expectedEventV2OrchestrationValidationException =
                new EventV2OrchestrationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: notFoundEventAddressV2OrchestrationException);

            this.eventAddressV2ProcessingServiceMock.Setup(broker =>
                broker.RetrieveEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(nullEventAddressV2);

            // when
            ValueTask<EventV1> submitEventV2Task =
                this.eventV2OrchestrationService.SubmitEventV2Async(someEventV2);

            EventV2OrchestrationValidationException actualEventV2OrchestrationValidationException =
                await Assert.ThrowsAsync<EventV2OrchestrationValidationException>(
                    submitEventV2Task.AsTask);

            // then
            actualEventV2OrchestrationValidationException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationValidationException);

            this.eventAddressV2ProcessingServiceMock.Verify(broker =>
                broker.RetrieveEventAddressV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationValidationException))),
                        Times.Once);

            this.eventV2ProcessingServiceMock.Verify(broker =>
                broker.AddEventV2Async(
                    It.IsAny<EventV1>()),
                        Times.Never);

            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
