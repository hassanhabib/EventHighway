// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
                broker.UpdateEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
