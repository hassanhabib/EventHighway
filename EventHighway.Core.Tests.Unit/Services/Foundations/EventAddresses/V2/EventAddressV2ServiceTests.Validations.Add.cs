// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
    }
}
