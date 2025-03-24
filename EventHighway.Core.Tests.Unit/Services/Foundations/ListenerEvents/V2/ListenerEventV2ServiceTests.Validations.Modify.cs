// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1.Exceptions;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.ListenerEvents.V2
{
    public partial class ListenerEventV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfListenerEventV2IsNullAndLogItAsync()
        {
            // given
            ListenerEventV1 nullListenerEventV2 = null;

            var nullListenerEventV2Exception =
                new NullListenerEventV2Exception(message: "Listener event is null.");

            var expectedListenerEventV2ValidationException =
                new ListenerEventV2ValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: nullListenerEventV2Exception);

            // when
            ValueTask<ListenerEventV1> modifyListenerEventV2Task =
                this.listenerEventV2Service.ModifyListenerEventV2Async(nullListenerEventV2);

            ListenerEventV2ValidationException actualListenerEventV2ValidationException =
                await Assert.ThrowsAsync<ListenerEventV2ValidationException>(
                    modifyListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ValidationException.Should().BeEquivalentTo(
                expectedListenerEventV2ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateListenerEventV2Async(It.IsAny<ListenerEventV1>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        private async Task ShouldThrowValidationExceptionOnModifyIfListenerEventV2IsInvalidAndLogItAsync(
            string invalidText)
        {
            ListenerEventV1Status invalidListenerEventV2Status =
                GetInvalidEnum<ListenerEventV1Status>();

            var invalidListenerEventV2 = new ListenerEventV1
            {
                Id = Guid.Empty,
                Response = invalidText,
                Status = invalidListenerEventV2Status,
                EventId = Guid.Empty,
                EventAddressId = Guid.Empty,
                EventListenerId = Guid.Empty
            };

            var invalidListenerEventV2Exception =
                new InvalidListenerEventV2Exception(
                    message: "Listener event is invalid, fix the errors and try again.");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.Id),
                values: "Required");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.Response),
                values: "Required");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.EventId),
                values: "Required");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.EventAddressId),
                values: "Required");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.EventListenerId),
                values: "Required");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.Status),
                values: "Value is not recognized");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.CreatedDate),
                values: "Required");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.UpdatedDate),

                values:
                    [
                        "Required",
                        $"Date is the same as {nameof(ListenerEventV1.CreatedDate)}"
                    ]);

            var expectedListenerEventV2ValidationException =
                new ListenerEventV2ValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: invalidListenerEventV2Exception);

            // when
            ValueTask<ListenerEventV1> modifyListenerEventV2Task =
                this.listenerEventV2Service.ModifyListenerEventV2Async(invalidListenerEventV2);

            ListenerEventV2ValidationException actualListenerEventV2ValidationException =
                await Assert.ThrowsAsync<ListenerEventV2ValidationException>(
                    modifyListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ValidationException.Should().BeEquivalentTo(
                expectedListenerEventV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateListenerEventV2Async(It.IsAny<ListenerEventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfUpdatedDateIsSameAsCreatedDateAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            ListenerEventV1 randomListenerEventV2 = CreateRandomListenerEventV2(dates: randomDateTimeOffset);
            ListenerEventV1 invalidListenerEventV2 = randomListenerEventV2;

            var invalidListenerEventV2Exception =
                new InvalidListenerEventV2Exception(
                    message: "Listener event is invalid, fix the errors and try again.");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.UpdatedDate),
                values: $"Date is the same as {nameof(ListenerEventV1.CreatedDate)}");

            var expectedListenerEventV2ValidationException =
                new ListenerEventV2ValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: invalidListenerEventV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            // when
            ValueTask<ListenerEventV1> modifyListenerEventV2Task =
                this.listenerEventV2Service.ModifyListenerEventV2Async(invalidListenerEventV2);

            ListenerEventV2ValidationException actualListenerEventV2ValidationException =
                await Assert.ThrowsAsync<ListenerEventV2ValidationException>(
                    modifyListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateListenerEventV2Async(It.IsAny<ListenerEventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(MinutesBeforeAndAfterNow))]
        public async Task ShouldThrowValidationExceptionOnModifyIfUpdatedDateIsNotRecentAndLogItAsync(
            int minutesBeforeOrAfter)
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();

            ListenerEventV1 randomListenerEventV2 =
                CreateRandomListenerEventV2(dates: randomDateTimeOffset);

            ListenerEventV1 invalidListenerEventV2 = randomListenerEventV2;

            invalidListenerEventV2.UpdatedDate =
                invalidListenerEventV2.UpdatedDate.AddMinutes(minutesBeforeOrAfter);

            var invalidListenerEventV2Exception =
                new InvalidListenerEventV2Exception(
                    message: "Listener event is invalid, fix the errors and try again.");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.UpdatedDate),
                values: "Date is not recent");

            var expectedListenerEventV2ValidationException =
                new ListenerEventV2ValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: invalidListenerEventV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            // when
            ValueTask<ListenerEventV1> modifyListenerEventV2Task =
                this.listenerEventV2Service.ModifyListenerEventV2Async(invalidListenerEventV2);

            ListenerEventV2ValidationException actualListenerEventV2ValidationException =
                await Assert.ThrowsAsync<ListenerEventV2ValidationException>(
                    modifyListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateListenerEventV2Async(It.IsAny<ListenerEventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfListenerEventV2DoesNotExistAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            int randomDaysAgo = GetRandomNegativeNumber();
            ListenerEventV1 randomListenerEventV2 = CreateRandomListenerEventV2(dates: randomDateTimeOffset);
            ListenerEventV1 nonExistListenerEventV2 = randomListenerEventV2;
            Guid nonExistListenerEventV2Id = nonExistListenerEventV2.Id;
            nonExistListenerEventV2.CreatedDate = randomDateTimeOffset.AddDays(randomDaysAgo);
            ListenerEventV1 nullListenerEventV2 = null;

            var notFoundListenerEventV2Exception =
                new NotFoundListenerEventV2Exception(
                    message: $"Could not find listener event with id: {nonExistListenerEventV2Id}.");

            var expectedListenerEventV2ValidationException =
                new ListenerEventV2ValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: notFoundListenerEventV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectListenerEventV2ByIdAsync(nonExistListenerEventV2Id))
                    .ReturnsAsync(nullListenerEventV2);

            // when
            ValueTask<ListenerEventV1> modifyListenerEventV2Task =
                this.listenerEventV2Service.ModifyListenerEventV2Async(nonExistListenerEventV2);

            ListenerEventV2ValidationException actualListenerEventV2ValidationException =
                await Assert.ThrowsAsync<ListenerEventV2ValidationException>(
                    modifyListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(nonExistListenerEventV2Id),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateListenerEventV2Async(It.IsAny<ListenerEventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfStorageCreatedDateNotSameAsCreatedDateAndLogItAsync()
        {
            // given
            int randomDaysAgo = GetRandomNegativeNumber();
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            ListenerEventV1 randomListenerEventV2 = CreateRandomListenerEventV2(dates: randomDateTimeOffset);
            ListenerEventV1 invalidListenerEventV2 = randomListenerEventV2;
            Guid invalidListenerEventV2Id = invalidListenerEventV2.Id;
            invalidListenerEventV2.CreatedDate = randomDateTimeOffset.AddDays(randomDaysAgo);
            DateTimeOffset randomOtherDateTime = GetRandomDateTimeOffset();
            ListenerEventV1 storageListenerEventV2 = invalidListenerEventV2.DeepClone();
            storageListenerEventV2.CreatedDate = randomOtherDateTime;

            var invalidListenerEventV2Exception =
                new InvalidListenerEventV2Exception(
                    message: "Listener event is invalid, fix the errors and try again.");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.CreatedDate),
                values: $"Date is not the same as storage");

            var expectedListenerEventV2ValidationException =
                new ListenerEventV2ValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: invalidListenerEventV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectListenerEventV2ByIdAsync(invalidListenerEventV2Id))
                    .ReturnsAsync(storageListenerEventV2);

            // when
            ValueTask<ListenerEventV1> modifyListenerEventV2Task =
                this.listenerEventV2Service.ModifyListenerEventV2Async(invalidListenerEventV2);

            ListenerEventV2ValidationException actualListenerEventV2ValidationException =
                await Assert.ThrowsAsync<ListenerEventV2ValidationException>(
                    modifyListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(invalidListenerEventV2Id),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfUpdatedDateIsEarlierThanStorageAndLogItAsync()
        {
            // given
            int randomTimeAgo = GetRandomNegativeNumber();
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            DateTimeOffset earlierDateTime = randomDateTimeOffset.AddDays(days: randomTimeAgo);
            ListenerEventV1 randomListenerEventV2 = CreateRandomListenerEventV2(dates: randomDateTimeOffset);
            ListenerEventV1 invalidListenerEventV2 = randomListenerEventV2;
            Guid invalidListenerEventV2Id = invalidListenerEventV2.Id;
            invalidListenerEventV2.CreatedDate = earlierDateTime;
            ListenerEventV1 storageListenerEventV2 = invalidListenerEventV2.DeepClone();
            DateTimeOffset earlierSeconds = randomDateTimeOffset.AddSeconds(seconds: randomTimeAgo);
            invalidListenerEventV2.UpdatedDate = earlierSeconds;

            var invalidListenerEventV2Exception =
                new InvalidListenerEventV2Exception(
                    message: "Listener event is invalid, fix the errors and try again.");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV1.UpdatedDate),
                values: $"Date is earlier than storage");

            var expectedListenerEventV2ValidationException =
                new ListenerEventV2ValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: invalidListenerEventV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectListenerEventV2ByIdAsync(invalidListenerEventV2Id))
                    .ReturnsAsync(storageListenerEventV2);

            // when
            ValueTask<ListenerEventV1> modifyListenerEventV2Task =
                this.listenerEventV2Service.ModifyListenerEventV2Async(invalidListenerEventV2);

            ListenerEventV2ValidationException actualListenerEventV2ValidationException =
                await Assert.ThrowsAsync<ListenerEventV2ValidationException>(
                    modifyListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(invalidListenerEventV2Id),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
