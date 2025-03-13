// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventListenerV2Id = Guid.Empty;

            var invalidEventListenerV2Exception =
                new InvalidEventListenerV2Exception(
                    message: "Event listener is invalid, fix the errors and try again.");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV2.Id),
                values: "Required");

            var expectedEventListenerV2ValidationException =
                new EventListenerV2ValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: invalidEventListenerV2Exception);

            // when
            ValueTask<EventListenerV2> removeEventListenerV2ByIdTask =
                this.eventListenerV2Service.RemoveEventListenerV2ByIdAsync(
                    invalidEventListenerV2Id);

            EventListenerV2ValidationException actualEventListenerV2ValidationException =
                await Assert.ThrowsAsync<EventListenerV2ValidationException>(
                    removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2ValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventListenerV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
