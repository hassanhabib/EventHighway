// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents.V2
{
    public partial class ListenerEventV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidListenerEventV2Id = Guid.Empty;

            var invalidListenerEventV2Exception =
                new InvalidListenerEventV2ProcessingException(
                    message: "Listener event is invalid, fix the errors and try again.");

            invalidListenerEventV2Exception.AddData(
                key: nameof(ListenerEventV2.Id),
                values: "Required");

            var expectedListenerEventV2ProcessingValidationException =
                new ListenerEventV2ProcessingValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: invalidListenerEventV2Exception);

            // when
            ValueTask<ListenerEventV2> removeListenerEventV2ByIdTask =
                this.listenerEventV2ProcessingService
                    .RemoveListenerEventV2ByIdAsync(
                        invalidListenerEventV2Id);

            ListenerEventV2ProcessingValidationException
                actualListenerEventV2ProcessingValidationException =
                    await Assert.ThrowsAsync<ListenerEventV2ProcessingValidationException>(
                        removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2ProcessingValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ProcessingValidationException))),
                        Times.Once);

            this.listenerEventV2ServiceMock.Verify(broker =>
                broker.RemoveListenerEventV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.listenerEventV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
