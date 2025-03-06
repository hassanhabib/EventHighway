// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents
{
    public partial class ListenerEventProcessingServiceTests
    {
        [Fact]
        public async Task ShouldAddListenerEventAsync()
        {
            // given
            ListenerEvent randomListenerEvent =
                CreateRandomListenerEvent();

            ListenerEvent inputListenerEvent =
                randomListenerEvent;

            ListenerEvent addedListenerEvent =
                inputListenerEvent;

            ListenerEvent expectedListenerEvent =
                addedListenerEvent.DeepClone();

            this.listenerEventServiceMock.Setup(broker =>
                broker.AddListenerEventAsync(inputListenerEvent))
                    .ReturnsAsync(addedListenerEvent);

            // when
            ListenerEvent actualListenerEvent =
                await this.listenerEventProcessingService
                    .AddListenerEventAsync(inputListenerEvent);

            // then
            actualListenerEvent.Should().BeEquivalentTo(
                expectedListenerEvent);

            this.listenerEventServiceMock.Verify(broker =>
                broker.AddListenerEventAsync(inputListenerEvent),
                    Times.Once);

            this.listenerEventServiceMock.VerifyNoOtherCalls();
        }
    }
}
