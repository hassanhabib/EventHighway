// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions;
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
            EventListenerV1 nullEventListenerV2 = null;

            var nullEventListenerV2Exception =
                new NullEventListenerV2Exception(message: "Event listener is null.");

            var expectedEventListenerV2ValidationException =
                new EventListenerV2ValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: nullEventListenerV2Exception);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
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
                broker.InsertEventListenerV1Async(It.IsAny<EventListenerV1>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        private async Task ShouldThrowValidationExceptionOnAddIfEventListenerV2IsInvalidAndLogItAsync(
            string invalidText)
        {
            var invalidEventListenerV2 = new EventListenerV1
            {
                Id = Guid.Empty,
                Name = invalidText,
                Description = invalidText,
                HeaderSecret = invalidText,
                Endpoint = invalidText,
                EventAddressId = Guid.Empty
            };

            var invalidEventListenerV2Exception =
                new InvalidEventListenerV2Exception(
                    message: "Event listener is invalid, fix the errors and try again.");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.Id),
                values: "Required");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.Name),
                values: "Required");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.Description),
                values: "Required");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.HeaderSecret),
                values: "Required");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.Endpoint),
                values: "Required");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.EventAddressId),
                values: "Required");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.CreatedDate),
                values: "Required");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.UpdatedDate),
                values: "Required");

            var expectedEventListenerV2ValidationException =
                new EventListenerV2ValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: invalidEventListenerV2Exception);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(invalidEventListenerV2);

            EventListenerV2ValidationException actualEventListenerV2ValidationException =
                await Assert.ThrowsAsync<EventListenerV2ValidationException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV1Async(It.IsAny<EventListenerV1>()),
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
            EventListenerV1 randomEventListenerV2 = CreateRandomEventListenerV2(dates: randomDateTimeOffset);
            EventListenerV1 invalidEventListenerV2 = randomEventListenerV2;
            invalidEventListenerV2.UpdatedDate = anotherRandomDateTimeOffset;

            var invalidEventListenerV2Exception =
                new InvalidEventListenerV2Exception(
                    message: "Event listener is invalid, fix the errors and try again.");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.CreatedDate),
                values: $"Date is not the same as {nameof(EventListenerV1.UpdatedDate)}");

            var expectedEventListenerV2ValidationException =
                new EventListenerV2ValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: invalidEventListenerV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(invalidEventListenerV2);

            EventListenerV2ValidationException actualEventListenerV2ValidationException =
                await Assert.ThrowsAsync<EventListenerV2ValidationException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV1Async(It.IsAny<EventListenerV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(MinutesBeforeAndAfterNow))]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotRecentAndLogItAsync(
            int minutesBeforeAndAfter)
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();

            EventListenerV1 randomEventListenerV2 =
                CreateRandomEventListenerV2(randomDateTimeOffset
                    .AddMinutes(minutesBeforeAndAfter));

            EventListenerV1 invalidEventListenerV2 = randomEventListenerV2;

            var invalidEventListenerV2Exception =
                new InvalidEventListenerV2Exception(
                    message: "Event listener is invalid, fix the errors and try again.");

            invalidEventListenerV2Exception.AddData(
                key: nameof(EventListenerV1.CreatedDate),
                values: "Date is not recent");

            var expectedEventListenerV2ValidationException =
                new EventListenerV2ValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: invalidEventListenerV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(invalidEventListenerV2);

            EventListenerV2ValidationException actualEventListenerV2ValidationException =
                await Assert.ThrowsAsync<EventListenerV2ValidationException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV1Async(It.IsAny<EventListenerV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
