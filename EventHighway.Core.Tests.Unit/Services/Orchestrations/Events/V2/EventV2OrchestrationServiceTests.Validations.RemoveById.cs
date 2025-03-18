// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Models.Services.Orchestrations.Events.V2.Exceptions;
using Moq;
using System.Threading.Tasks;
using System;
using FluentAssertions;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventV2Id = Guid.Empty;

            var invalidEventV2Exception =
                new InvalidEventV2OrchestrationException(
                    message: "Event is invalid, fix the errors and try again.");

            invalidEventV2Exception.AddData(
                key: nameof(EventV2.Id),
                values: "Required");

            var expectedEventV2OrchestrationValidationException =
                new EventV2OrchestrationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: invalidEventV2Exception);

            // when
            ValueTask<EventV2> removeEventV2ByIdTask =
                this.eventV2OrchestrationService
                    .RemoveEventV2ByIdAsync(
                        invalidEventV2Id);

            EventV2OrchestrationValidationException
                actualEventV2OrchestrationValidationException =
                    await Assert.ThrowsAsync<EventV2OrchestrationValidationException>(
                        removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2OrchestrationValidationException.Should()
                .BeEquivalentTo(expectedEventV2OrchestrationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2OrchestrationValidationException))),
                        Times.Once);

            this.eventV2ProcessingServiceMock.Verify(broker =>
                broker.RemoveEventV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
