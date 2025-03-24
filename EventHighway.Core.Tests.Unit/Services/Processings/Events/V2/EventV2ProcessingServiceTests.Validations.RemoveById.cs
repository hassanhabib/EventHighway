// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Processings.Events.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.Events.V2
{
    public partial class EventV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventV2Id = Guid.Empty;

            var invalidEventV2Exception =
                new InvalidEventV2ProcessingException(
                    message: "Event is invalid, fix the errors and try again.");

            invalidEventV2Exception.AddData(
                key: nameof(EventV1.Id),
                values: "Required");

            var expectedEventV2ProcessingValidationException =
                new EventV2ProcessingValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: invalidEventV2Exception);

            // when
            ValueTask<EventV1> removeEventV2ByIdTask =
                this.eventV2ProcessingService
                    .RemoveEventV2ByIdAsync(
                        invalidEventV2Id);

            EventV2ProcessingValidationException
                actualEventV2ProcessingValidationException =
                    await Assert.ThrowsAsync<EventV2ProcessingValidationException>(
                        removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2ProcessingValidationException.Should()
                .BeEquivalentTo(expectedEventV2ProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2ProcessingValidationException))),
                        Times.Once);

            this.eventV2ServiceMock.Verify(broker =>
                broker.RemoveEventV1ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
