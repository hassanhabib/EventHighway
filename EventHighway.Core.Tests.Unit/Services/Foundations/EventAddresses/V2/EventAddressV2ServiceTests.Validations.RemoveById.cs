// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventAddresses.V2
{
    public partial class EventAddressV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventAddressV2Id = Guid.Empty;

            var invalidEventAddressV2Exception =
                new InvalidEventAddressV2Exception(
                    message: "Event address is invalid, fix the errors and try again.");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV1.Id),
                values: "Required");

            var expectedEventAddressV2ValidationException =
                new EventAddressV2ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: invalidEventAddressV2Exception);

            // when
            ValueTask<EventAddressV1> removeEventAddressV2ByIdTask =
                this.eventAddressV2Service.RemoveEventAddressV2ByIdAsync(
                    invalidEventAddressV2Id);

            EventAddressV2ValidationException actualEventAddressV2ValidationException =
                await Assert.ThrowsAsync<EventAddressV2ValidationException>(
                    removeEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2ValidationException.Should()
                .BeEquivalentTo(expectedEventAddressV2ValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventAddressV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfEventAddressV2IsNotFoundAndLogItAsync()
        {
            // given
            Guid nonExistingEventAddressV2Id = GetRandomId();
            EventAddressV1 nullEventAddressV2 = null;

            var notFoundEventAddressV2Exception =
                new NotFoundEventAddressV2Exception(
                    message: $"Could not find event address with id: {nonExistingEventAddressV2Id}.");

            var expectedEventAddressV2ValidationException =
                new EventAddressV2ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: notFoundEventAddressV2Exception);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(nullEventAddressV2);

            // when
            ValueTask<EventAddressV1> removeEventAddressV2ByIdTask =
                this.eventAddressV2Service.RemoveEventAddressV2ByIdAsync(nonExistingEventAddressV2Id);

            EventAddressV2ValidationException actualEventAddressV2ValidationException =
                await Assert.ThrowsAsync<EventAddressV2ValidationException>(
                    removeEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2ValidationException.Should()
                .BeEquivalentTo(expectedEventAddressV2ValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventAddressV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2ValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteEventAddressV2Async(
                    It.IsAny<EventAddressV1>()),
                        Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
