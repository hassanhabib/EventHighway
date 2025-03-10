// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents
{
    public partial class ListenerEventProcessingServiceTests
    {
        [Fact]
        public async Task ShouldModifyListenerEventAsync()
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

            this.listenerEventServiceMock.Setup(service =>
                service.ModifyListenerEventAsync(inputListenerEvent))
                    .ReturnsAsync(addedListenerEvent);

            // when
            ListenerEvent actualListenerEvent =
                await this.listenerEventProcessingService
                    .ModifyListenerEventAsync(inputListenerEvent);

            // then
            actualListenerEvent.Should().BeEquivalentTo(
                expectedListenerEvent);

            this.listenerEventServiceMock.Verify(service =>
                service.ModifyListenerEventAsync(inputListenerEvent),
                    Times.Once);

            this.listenerEventServiceMock.VerifyNoOtherCalls();
        }
    }
}
