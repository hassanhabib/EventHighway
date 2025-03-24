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
        public async Task ShouldModifyListenerEventV2Async()
        {
            // given
            ListenerEventV1 randomListenerEventV2 =
                CreateRandomListenerEventV2();

            ListenerEventV1 inputListenerEventV2 =
                randomListenerEventV2;

            ListenerEventV1 modifiedListenerEventV2 =
                inputListenerEventV2;

            ListenerEventV1 expectedListenerEventV2 =
                modifiedListenerEventV2.DeepClone();

            this.listenerEventV2ProcessingServiceMock.Setup(service =>
                service.ModifyListenerEventV1Async(
                    inputListenerEventV2))
                        .ReturnsAsync(modifiedListenerEventV2);

            // when
            ListenerEventV1 actualListenerEventV2 =
                await this.eventListenerV2OrchestrationService
                    .ModifyListenerEventV2Async(
                        inputListenerEventV2);

            // then
            actualListenerEventV2.Should().BeEquivalentTo(
                expectedListenerEventV2);

            this.listenerEventV2ProcessingServiceMock.Verify(service =>
                service.ModifyListenerEventV1Async(
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
