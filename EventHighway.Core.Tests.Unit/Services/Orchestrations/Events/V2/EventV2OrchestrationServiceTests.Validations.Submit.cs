// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
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
            EventV2 nullEventV2 = null;

            var nullEventV2OrchestrationException =
                new NullEventV2OrchestrationException(message: "Event is null.");

            var expectedEventV2OrchestrationValidationException =
                new EventV2OrchestrationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: nullEventV2OrchestrationException);

            // when
            ValueTask<EventV2> submitEventV2Task =
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
                    It.IsAny<EventV2>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
