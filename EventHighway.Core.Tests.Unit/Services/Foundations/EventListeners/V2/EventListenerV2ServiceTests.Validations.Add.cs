// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
        public async Task ShouldThrowValidationExceptionOnAddIfEventListenerV2IsNullAndLogItAsync()
        {
            // given
            EventListenerV2 nullEventListenerV2 = null;

            var nullEventListenerV2Exception =
                new NullEventListenerV2Exception(message: "Event listener is null.");

            var expectedEventListenerV2ValidationException =
                new EventListenerV2ValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: nullEventListenerV2Exception);

            // when
            ValueTask<EventListenerV2> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(nullEventListenerV2);

            EventListenerV2ValidationException actualEventListenerV2ValidationException =
                await Assert.ThrowsAsync<EventListenerV2ValidationException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV2>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
