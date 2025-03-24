// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
        public async Task ShouldThrowValidationExceptionOnAddIfEventListenerV2IsNullAndLogItAsync()
        {
            // given
            EventListenerV1 nullEventListenerV2 = null;

            var nullEventListenerV2ProcessingException =
                new NullEventListenerV2ProcessingException(message: "Event listener is null.");

            var expectedEventListenerV2ProcessingValidationException =
                new EventListenerV2ProcessingValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: nullEventListenerV2ProcessingException);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2ProcessingService.AddEventListenerV2Async(nullEventListenerV2);

            EventListenerV2ProcessingValidationException
                actualEventListenerV2ProcessingValidationException =
                    await Assert.ThrowsAsync<EventListenerV2ProcessingValidationException>(
                        addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ProcessingValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2ProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ProcessingValidationException))),
                        Times.Once);

            this.eventListenerV2ServiceMock.Verify(broker =>
                broker.AddEventListenerV1Async(
                    It.IsAny<EventListenerV1>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
