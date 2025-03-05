// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.EventCall.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventCalls.V2
{
    public partial class EventCallV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfEventCallV2IsNullAndLogItAsync()
        {
            // given
            EventCallV2 nullEventCallV2 = null;

            var nullEventCallV2Exception =
                new NullEventCallV2Exception(message: "Event call is null.");

            var expectedEventCallV2ValidationException =
                new EventCallV2ValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: nullEventCallV2Exception);

            // when
            ValueTask<EventCallV2> addEventCallV2Task =
                this.eventCallV2Service.RunEventCallV2Async(nullEventCallV2);

            EventCallV2ValidationException actualEventCallV2ValidationException =
                await Assert.ThrowsAsync<EventCallV2ValidationException>(
                    addEventCallV2Task.AsTask);

            // then
            actualEventCallV2ValidationException.Should().BeEquivalentTo(
                expectedEventCallV2ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV2ValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        private async Task ShouldThrowValidationExceptionOnAddIfEventCallV2IsInvalidAndLogItAsync(
            string invalidText)
        {
            var invalidEventCallV2 = new EventCallV2
            {
                Endpoint = invalidText,
                Content = invalidText
            };

            var invalidEventCallV2Exception =
                new InvalidEventCallV2Exception(
                    message: "Event call is invalid, fix the errors and try again.");

            invalidEventCallV2Exception.AddData(
                key: nameof(EventCallV2.Endpoint),
                values: "Required");

            invalidEventCallV2Exception.AddData(
                key: nameof(EventCallV2.Content),
                values: "Required");

            var expectedEventCallV2ValidationException =
                new EventCallV2ValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: invalidEventCallV2Exception);

            // when
            ValueTask<EventCallV2> addEventCallV2Task =
                this.eventCallV2Service.RunEventCallV2Async(invalidEventCallV2);

            EventCallV2ValidationException actualEventCallV2ValidationException =
                await Assert.ThrowsAsync<EventCallV2ValidationException>(
                    addEventCallV2Task.AsTask);

            // then
            actualEventCallV2ValidationException.Should().BeEquivalentTo(
                expectedEventCallV2ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventCallV2ValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
