// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRunIfEventCallV2IsNullAndLogItAsync()
        {
            // given
            EventCallV2 nullEventCallV2 = null;

            var nullEventCallV2OrchestrationException =
                new NullEventCallV2OrchestrationException(message: "Event call is null.");

            var expectedEventV2OrchestrationValidationException =
                new EventV2OrchestrationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: nullEventCallV2OrchestrationException);

            // when
            ValueTask<EventCallV2> runEventCallV2Task =
                this.eventV2OrchestrationService.RunEventCallV2Async(nullEventCallV2);

            EventV2OrchestrationValidationException
                actualEventV2OrchestrationValidationException =
                    await Assert.ThrowsAsync<EventV2OrchestrationValidationException>(
                        runEventCallV2Task.AsTask);

            // then
            actualEventV2OrchestrationValidationException.Should().BeEquivalentTo(
                expectedEventV2OrchestrationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationValidationException))),
                        Times.Once);

            this.eventCallV2ProcessingServiceMock.Verify(broker =>
                broker.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
