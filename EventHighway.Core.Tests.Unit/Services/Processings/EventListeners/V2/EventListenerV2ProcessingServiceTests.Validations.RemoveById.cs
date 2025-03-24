// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Processings.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventListeners.V2
{
    public partial class EventListenerV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventListenerV2Id = Guid.Empty;

            var invalidEventListenerV2Exception =
                new InvalidEventListenerV2ProcessingException(
                    message: "Event listener is invalid, fix the errors and try again.");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.Id),
                values: "Required");

            var expectedEventListenerV2ProcessingValidationException =
                new EventListenerV2ProcessingValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: invalidEventListenerV2Exception);

            // when
            ValueTask<EventListenerV1> removeEventListenerV2ByIdTask =
                this.eventListenerV2ProcessingService
                    .RemoveEventListenerV2ByIdAsync(
                        invalidEventListenerV2Id);

            EventListenerV2ProcessingValidationException
                actualEventListenerV2ProcessingValidationException =
                    await Assert.ThrowsAsync<EventListenerV2ProcessingValidationException>(
                        removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2ProcessingValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2ProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ProcessingValidationException))),
                        Times.Once);

            this.eventListenerV2ServiceMock.Verify(broker =>
                broker.RemoveEventListenerV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
