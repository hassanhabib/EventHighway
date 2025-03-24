// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldAddListenerEventV2Async()
        {
            // given
            ListenerEventV1 randomListenerEventV2 =
                CreateRandomListenerEventV2();

            ListenerEventV1 inputListenerEventV2 =
                randomListenerEventV2;

            ListenerEventV1 addedListenerEventV2 =
                inputListenerEventV2;

            ListenerEventV1 expectedListenerEventV2 =
                addedListenerEventV2.DeepClone();

            this.listenerEventV2ProcessingServiceMock.Setup(service =>
                service.AddListenerEventV1Async(
                    inputListenerEventV2))
                        .ReturnsAsync(addedListenerEventV2);

            // when
            ListenerEventV1 actualListenerEventV2 =
                await this.eventListenerV2OrchestrationService
                    .AddListenerEventV2Async(
                        inputListenerEventV2);

            // then
            actualListenerEventV2.Should().BeEquivalentTo(
                expectedListenerEventV2);

            this.listenerEventV2ProcessingServiceMock.Verify(service =>
                service.AddListenerEventV1Async(
                    inputListenerEventV2),
                        Times.Once);

            this.listenerEventV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.eventListenerV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
