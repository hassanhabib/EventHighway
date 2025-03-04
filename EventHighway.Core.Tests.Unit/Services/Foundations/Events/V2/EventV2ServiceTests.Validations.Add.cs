// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV2Async(It.IsAny<EventV2>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
