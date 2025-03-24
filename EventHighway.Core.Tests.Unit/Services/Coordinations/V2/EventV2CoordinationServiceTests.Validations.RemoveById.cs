// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventV2Id = Guid.Empty;

            var invalidEventV2Exception =
                new InvalidEventV2CoordinationException(
                    message: "Event is invalid, fix the errors and try again.");

            invalidEventV2Exception.AddData(
                key: nameof(EventV1.Id),
                values: "Required");

            var expectedEventV2CoordinationValidationException =
                new EventV2CoordinationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: invalidEventV2Exception);

            // when
            ValueTask<EventV1> removeEventV2ByIdTask =
                this.eventV2CoordinationService
                    .RemoveEventV2ByIdAsync(
                        invalidEventV2Id);

            EventV2CoordinationValidationException
                actualEventV2CoordinationValidationException =
                    await Assert.ThrowsAsync<EventV2CoordinationValidationException>(
                        removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2CoordinationValidationException.Should()
                .BeEquivalentTo(expectedEventV2CoordinationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationValidationException))),
                        Times.Once);

            this.eventV2OrchestrationServiceMock.Verify(broker =>
                broker.RemoveEventV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
