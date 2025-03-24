// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
        public async Task ShouldThrowValidationExceptionOnAddIfEventV2IsNullAndLogItAsync()
        {
            // given
            EventV1 nullEventV2 = null;

            var nullEventV2ProcessingException =
                new NullEventV2ProcessingException(message: "Event is null.");

            var expectedEventV2ProcessingValidationException =
                new EventV2ProcessingValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: nullEventV2ProcessingException);

            // when
            ValueTask<EventV1> addEventV2Task =
                this.eventV2ProcessingService.AddEventV2Async(nullEventV2);

            EventV2ProcessingValidationException
                actualEventV2ProcessingValidationException =
                    await Assert.ThrowsAsync<EventV2ProcessingValidationException>(
                        addEventV2Task.AsTask);

            // then
            actualEventV2ProcessingValidationException.Should().BeEquivalentTo(
                expectedEventV2ProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2ProcessingValidationException))),
                        Times.Once);

            this.eventV2ServiceMock.Verify(broker =>
                broker.AddEventV2Async(
                    It.IsAny<EventV1>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
