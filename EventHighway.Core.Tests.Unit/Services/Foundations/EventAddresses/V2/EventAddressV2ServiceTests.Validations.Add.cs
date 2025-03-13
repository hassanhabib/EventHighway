// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventAddresses.V2
{
    public partial class EventAddressV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfEventAddressV2IsNullAndLogItAsync()
        {
            // given
            EventAddressV2 nullEventAddressV2 = null;

            var nullEventAddressV2Exception =
                new NullEventAddressV2Exception(message: "Event address is null.");

            var expectedEventAddressV2ValidationException =
                new EventAddressV2ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: nullEventAddressV2Exception);

            // when
            ValueTask<EventAddressV2> addEventAddressV2Task =
                this.eventAddressV2Service.AddEventAddressV2Async(nullEventAddressV2);

            EventAddressV2ValidationException actualEventAddressV2ValidationException =
                await Assert.ThrowsAsync<EventAddressV2ValidationException>(
                    addEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2ValidationException.Should().BeEquivalentTo(
                expectedEventAddressV2ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2ValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressV2Async(It.IsAny<EventAddressV2>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        private async Task ShouldThrowValidationExceptionOnAddIfEventAddressV2IsInvalidAndLogItAsync(
            string invalidText)
        {
            var invalidEventAddressV2 = new EventAddressV2
            {
                Id = Guid.Empty,
                Name = invalidText,
                Description = invalidText,
            };

            var invalidEventAddressV2Exception =
                new InvalidEventAddressV2Exception(
                    message: "Event address is invalid, fix the errors and try again.");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV2.Id),
                values: "Required");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV2.Name),
                values: "Required");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV2.Description),
                values: "Required");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV2.CreatedDate),
                values: "Required");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV2.UpdatedDate),
                values: "Required");

            var expectedEventAddressV2ValidationException =
                new EventAddressV2ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: invalidEventAddressV2Exception);

            // when
            ValueTask<EventAddressV2> addEventAddressV2Task =
                this.eventAddressV2Service.AddEventAddressV2Async(invalidEventAddressV2);

            EventAddressV2ValidationException actualEventAddressV2ValidationException =
                await Assert.ThrowsAsync<EventAddressV2ValidationException>(
                    addEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2ValidationException.Should().BeEquivalentTo(
                expectedEventAddressV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressV2Async(It.IsAny<EventAddressV2>()),
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
            EventAddressV2 randomEventAddressV2 = CreateRandomEventAddressV2(dates: randomDateTimeOffset);
            EventAddressV2 invalidEventAddressV2 = randomEventAddressV2;
            invalidEventAddressV2.UpdatedDate = anotherRandomDateTimeOffset;

            var invalidEventAddressV2Exception =
                new InvalidEventAddressV2Exception(
                    message: "Event address is invalid, fix the errors and try again.");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV2.CreatedDate),
                values: $"Date is not the same as {nameof(EventAddressV2.UpdatedDate)}");

            var expectedEventAddressV2ValidationException =
                new EventAddressV2ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: invalidEventAddressV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            // when
            ValueTask<EventAddressV2> addEventAddressV2Task =
                this.eventAddressV2Service.AddEventAddressV2Async(invalidEventAddressV2);

            EventAddressV2ValidationException actualEventAddressV2ValidationException =
                await Assert.ThrowsAsync<EventAddressV2ValidationException>(
                    addEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2ValidationException.Should().BeEquivalentTo(
                expectedEventAddressV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressV2Async(It.IsAny<EventAddressV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(MinutesBeforeAndAfterNow))]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotRecentAndLogItAsync(
            int minutesBeforeAndAfterNow)
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();

            EventAddressV2 randomEventAddressV2 =
                CreateRandomEventAddressV2(
                    dates: randomDateTimeOffset.AddMinutes(minutesBeforeAndAfterNow));

            EventAddressV2 invalidEventAddressV2 = randomEventAddressV2;

            var invalidEventAddressV2Exception =
                new InvalidEventAddressV2Exception(
                    message: "Event address is invalid, fix the errors and try again.");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV2.CreatedDate),
                values: "Date is not recent");

            var expectedEventAddressV2ValidationException =
                new EventAddressV2ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: invalidEventAddressV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            // when
            ValueTask<EventAddressV2> addEventAddressV2Task =
                this.eventAddressV2Service.AddEventAddressV2Async(invalidEventAddressV2);

            EventAddressV2ValidationException actualEventAddressV2ValidationException =
                await Assert.ThrowsAsync<EventAddressV2ValidationException>(
                    addEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2ValidationException.Should().BeEquivalentTo(
                expectedEventAddressV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressV2Async(It.IsAny<EventAddressV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
