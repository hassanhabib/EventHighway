// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
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
        public async Task ShouldThrowValidationOnRetrieveByEventAddressIdIfEventAddressIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventAddressId = Guid.Empty;

            var invalidEventListenerV2ProcessingException =
                new InvalidEventListenerV2ProcessingException(
                    message: "Event listener is invalid, fix the errors and try again.");

            invalidEventListenerV2ProcessingException.AddData(
                key: nameof(EventListenerV1.EventAddressId),
                values: "Required");

            var expectedEventListenerV2ProcessingValidationException =
                new EventListenerV2ProcessingValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: invalidEventListenerV2ProcessingException);

            // when
            ValueTask<IQueryable<EventListenerV1>> retrieveEventListenerV2sByEventAddressIdTask =
                this.eventListenerV2ProcessingService.RetrieveEventListenerV2sByEventAddressIdAsync(
                    invalidEventAddressId);

            EventListenerV2ProcessingValidationException actualEventListenerV2ProcessingValidationException =
                await Assert.ThrowsAsync<EventListenerV2ProcessingValidationException>(
                    retrieveEventListenerV2sByEventAddressIdTask.AsTask);

            // then
            actualEventListenerV2ProcessingValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2ProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ProcessingValidationException))),
                        Times.Once);

            this.eventListenerV2ServiceMock.Verify(service =>
                service.RetrieveAllEventListenerV2sAsync(),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
