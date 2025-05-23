// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V1
{
    public partial class EventV1ServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfEventV1IsNullAndLogItAsync()
        {
            // given
            EventV1 nullEventV1 = null;

            var nullEventV1Exception =
                new NullEventV1Exception(message: "Event is null.");

            var expectedEventV1ValidationException =
                new EventV1ValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: nullEventV1Exception);

            // when
            ValueTask<EventV1> modifyEventV1Task =
                this.eventV1Service.ModifyEventV1Async(nullEventV1);

            EventV1ValidationException actualEventV1ValidationException =
                await Assert.ThrowsAsync<EventV1ValidationException>(
                    modifyEventV1Task.AsTask);

            // then
            actualEventV1ValidationException.Should().BeEquivalentTo(
                expectedEventV1ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1ValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        private async Task ShouldThrowValidationExceptionOnModifyIfEventV1IsInvalidAndLogItAsync(
            string invalidText)
        {
            EventV1Type invalidEventV1Type = GetInvalidEnum<EventV1Type>();

            var invalidEventV1 = new EventV1
            {
                Id = Guid.Empty,
                Content = invalidText,
                Type = invalidEventV1Type,
                EventAddressId = Guid.Empty
            };

            var invalidEventV1Exception =
                new InvalidEventV1Exception(
                    message: "Event is invalid, fix the errors and try again.");

            invalidEventV1Exception.AddData(
                key: nameof(EventV1.Id),
                values: "Required");

            invalidEventV1Exception.AddData(
                key: nameof(EventV1.Content),
                values: "Required");

            invalidEventV1Exception.AddData(
                key: nameof(EventV1.EventAddressId),
                values: "Required");

            invalidEventV1Exception.AddData(
                key: nameof(EventV1.Type),
                values: "Value is not recognized");

            invalidEventV1Exception.AddData(
                key: nameof(EventV1.CreatedDate),
                values: "Required");

            invalidEventV1Exception.AddData(
                key: nameof(EventV1.UpdatedDate),
                values: "Required");

            var expectedEventV1ValidationException =
                new EventV1ValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: invalidEventV1Exception);

            // when
            ValueTask<EventV1> modifyEventV1Task =
                this.eventV1Service.ModifyEventV1Async(invalidEventV1);

            EventV1ValidationException actualEventV1ValidationException =
                await Assert.ThrowsAsync<EventV1ValidationException>(
                    modifyEventV1Task.AsTask);

            // then
            actualEventV1ValidationException.Should().BeEquivalentTo(
                expectedEventV1ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateEventV1Async(It.IsAny<EventV1>()),
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
            EventV1 randomEventV1 = CreateRandomEventV1(randomDateTimeOffset);
            EventV1 invalidEventV1 = randomEventV1;

            var invalidEventV1Exception =
                new InvalidEventV1Exception(
                    message: "Event is invalid, fix the errors and try again.");

            invalidEventV1Exception.AddData(
                key: nameof(EventV1.UpdatedDate),
                values: $"Date is the same as {nameof(EventV1.CreatedDate)}.");

            var expectedEventV1ValidationException =
                new EventV1ValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: invalidEventV1Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            // when
            ValueTask<EventV1> modifyEventV1Task =
                this.eventV1Service.ModifyEventV1Async(invalidEventV1);

            EventV1ValidationException actualEventV1ValidationException =
                await Assert.ThrowsAsync<EventV1ValidationException>(
                    modifyEventV1Task.AsTask);

            // then
            actualEventV1ValidationException.Should()
                .BeEquivalentTo(expectedEventV1ValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
               broker.GetDateTimeOffsetAsync(),
                   Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
