// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Models.Services.Processings.EventCalls.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventCalls.V2
{
    public partial class EventCallV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRunIfEventCallV2IsNullAndLogItAsync()
        {
            // given
            EventCallV1 nullEventCallV2 = null;

            var nullEventCallV2ProcessingException =
                new NullEventCallV2ProcessingException(message: "Event call is null.");

            var expectedEventCallV2ProcessingValidationException =
                new EventCallV2ProcessingValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: nullEventCallV2ProcessingException);

            // when
            ValueTask<EventCallV1> runEventCallV2Task =
                this.eventCallV2ProcessingService.RunEventCallV2Async(nullEventCallV2);

            EventCallV2ProcessingValidationException
                actualEventCallV2ProcessingValidationException =
                    await Assert.ThrowsAsync<EventCallV2ProcessingValidationException>(
                        runEventCallV2Task.AsTask);

            // then
            actualEventCallV2ProcessingValidationException.Should().BeEquivalentTo(
                expectedEventCallV2ProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV2ProcessingValidationException))),
                        Times.Once);

            this.eventCallV2ServiceMock.Verify(broker =>
                broker.RunEventCallV2Async(
                    It.IsAny<EventCallV1>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventCallV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
