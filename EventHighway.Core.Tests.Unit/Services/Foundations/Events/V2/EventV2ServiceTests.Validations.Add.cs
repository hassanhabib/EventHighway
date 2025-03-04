// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.Events.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V2
{
    public partial class EventV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfEventV2IsNullAndLogItAsync()
        {
            // given
            EventV2 nullEventV2 = null;

            var nullEventV2Exception =
                new NullEventV2Exception(message: "Event is null.");

            var expectedEventV2ValidationException =
                new EventV2ValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: nullEventV2Exception);

            // when
            ValueTask<EventV2> addEventV2Task =
                this.eventV2Service.AddEventV2Async(nullEventV2);

            EventV2ValidationException actualEventV2ValidationException =
                await Assert.ThrowsAsync<EventV2ValidationException>(
                    addEventV2Task.AsTask);

            // then
            actualEventV2ValidationException.Should().BeEquivalentTo(
                expectedEventV2ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2ValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV2Async(It.IsAny<EventV2>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        private async Task ShouldThrowValidationExceptionOnAddIfEventV2IsInvalidAndLogItAsync(
            string invalidText)
        {
            EventV2Type invalidEventV2Type = GetInvalidEnum<EventV2Type>();

            var invalidEventV2 = new EventV2
            {
                Id = Guid.Empty,
                Content = invalidText,
                Type = invalidEventV2Type,
                EventAddressId = Guid.Empty
            };

            var invalidEventV2Exception =
                new InvalidEventV2Exception(
                    message: "Event is invalid, fix the errors and try again.");

            invalidEventV2Exception.AddData(
                key: nameof(EventV2.Id),
                values: "Required");

            invalidEventV2Exception.AddData(
                key: nameof(EventV2.Content),
                values: "Required");

            invalidEventV2Exception.AddData(
                key: nameof(EventV2.EventAddressId),
                values: "Required");

            invalidEventV2Exception.AddData(
                key: nameof(EventV2.Type),
                values: "Value is not recognized.");

            invalidEventV2Exception.AddData(
                key: nameof(EventV2.CreatedDate),
                values: "Required");

            invalidEventV2Exception.AddData(
                key: nameof(EventV2.UpdatedDate),
                values: "Required");

            var expectedEventV2ValidationException =
                new EventV2ValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: invalidEventV2Exception);

            // when
            ValueTask<EventV2> addEventV2Task =
                this.eventV2Service.AddEventV2Async(invalidEventV2);

            EventV2ValidationException actualEventV2ValidationException =
                await Assert.ThrowsAsync<EventV2ValidationException>(
                    addEventV2Task.AsTask);

            // then
            actualEventV2ValidationException.Should().BeEquivalentTo(
                expectedEventV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV2Async(It.IsAny<EventV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            DateTimeOffset anotherRandomDateTimeOffset = GetRandomDateTimeOffset();
            EventV2 randomEventV2 = CreateRandomEventV2(randomDateTimeOffset);
            EventV2 invalidEventV2 = randomEventV2;
            invalidEventV2.UpdatedDate = anotherRandomDateTimeOffset;

            var invalidEventV2Exception =
                new InvalidEventV2Exception(
                    message: "Event is invalid, fix the errors and try again.");

            invalidEventV2Exception.AddData(
                key: nameof(EventV2.CreatedDate),
                values: $"Date is not the same as {nameof(EventV2.UpdatedDate)}.");

            var expectedEventV2ValidationException =
                new EventV2ValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: invalidEventV2Exception);

            // when
            ValueTask<EventV2> addEventV2Task =
                this.eventV2Service.AddEventV2Async(invalidEventV2);

            EventV2ValidationException actualEventV2ValidationException =
                await Assert.ThrowsAsync<EventV2ValidationException>(
                    addEventV2Task.AsTask);

            // then
            actualEventV2ValidationException.Should().BeEquivalentTo(
                expectedEventV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV2Async(It.IsAny<EventV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
