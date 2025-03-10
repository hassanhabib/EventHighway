// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.ListenerEvents
{
    public partial class ListenerEventServiceTests
    {
        [Fact]
        public async Task ShouldModifyListenerEventAsync()
        {
            // given
            ListenerEvent randomListenerEvent =
                CreateRandomListenerEvent();

            ListenerEvent inputListenerEvent =
                randomListenerEvent;

            ListenerEvent storageListenerEvent =
                inputListenerEvent;

            ListenerEvent expectedListenerEvent =
                storageListenerEvent.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateListenerEventAsync(inputListenerEvent))
                    .ReturnsAsync(storageListenerEvent);

            // when
            ListenerEvent actualListenerEvent =
                await this.listenerEventService.ModifyListenerEventAsync(
                    inputListenerEvent);

            // then
            actualListenerEvent.Should().BeEquivalentTo(
                expectedListenerEvent);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateListenerEventAsync(inputListenerEvent),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
