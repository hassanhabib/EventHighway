// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Models.Processings.ListenerEvents.V2.Exceptions;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents.V2
{
    public partial class ListenerEventV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfListenerEventV2IsNullAndLogItAsync()
        {
            // given
            ListenerEventV2 nullListenerEventV2 = null;

            var nullListenerEventV2ProcessingException =
                new NullListenerEventV2ProcessingException(message: "Listener event is null.");

            var expectedListenerEventV2ProcessingValidationException =
                new ListenerEventV2ProcessingValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: nullListenerEventV2ProcessingException);

            // when
            ValueTask<ListenerEventV2> addListenerEventV2Task =
                this.listenerEventV2ProcessingService.AddListenerEventV2Async(nullListenerEventV2);

            ListenerEventV2ProcessingValidationException
                actualListenerEventV2ProcessingValidationException =
                    await Assert.ThrowsAsync<ListenerEventV2ProcessingValidationException>(
                        addListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ProcessingValidationException.Should().BeEquivalentTo(
                expectedListenerEventV2ProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ProcessingValidationException))),
                        Times.Once);

            this.listenerEventV2ServiceMock.Verify(broker =>
                broker.AddListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.listenerEventV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
