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
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEventAddressV2Id = Guid.Empty;

            var invalidEventAddressV2Exception =
                new InvalidEventAddressV2Exception(
                    message: "Event address is invalid, fix the errors and try again.");

            invalidEventAddressV2Exception.AddData(
                key: nameof(EventAddressV2.Id),
                values: "Required");

            var expectedEventAddressV2ValidationException =
                new EventAddressV2ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: invalidEventAddressV2Exception);

            // when
            ValueTask<EventAddressV2> retrieveEventAddressV2ByIdTask =
                this.eventAddressV2Service.RetrieveEventAddressV2ByIdAsync(
                    invalidEventAddressV2Id);

            EventAddressV2ValidationException actualEventAddressV2ValidationException =
                await Assert.ThrowsAsync<EventAddressV2ValidationException>(
                    retrieveEventAddressV2ByIdTask.AsTask);

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
        }
    }
}
