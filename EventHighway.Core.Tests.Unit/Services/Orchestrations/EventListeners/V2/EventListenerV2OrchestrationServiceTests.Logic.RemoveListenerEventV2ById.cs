// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
        public async Task ShouldRemoveListenerEventV2ByIdAsync()
        {
            // given
            Guid randomListenerEventId = GetRandomId();
            Guid inputListenerEventId = randomListenerEventId;

            ListenerEventV1 randomListenerEventV2 =
                CreateRandomListenerEventV2();

            ListenerEventV1 removedListenerEventV2 =
                randomListenerEventV2;

            ListenerEventV1 expectedListenerEventV2 =
                removedListenerEventV2.DeepClone();

            this.listenerEventV2ProcessingServiceMock.Setup(service =>
                service.RemoveListenerEventV2ByIdAsync(
                    inputListenerEventId))
                        .ReturnsAsync(removedListenerEventV2);

            // when
            ListenerEventV1 actualListenerEventV2 =
                await this.eventListenerV2OrchestrationService
                    .RemoveListenerEventV2ByIdAsync(
                        inputListenerEventId);

            // then
            actualListenerEventV2.Should()
                .BeEquivalentTo(expectedListenerEventV2);

            this.listenerEventV2ProcessingServiceMock.Verify(service =>
                service.RemoveListenerEventV2ByIdAsync(
                    inputListenerEventId),
                        Times.Once);

            this.listenerEventV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.eventListenerV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock
                .VerifyNoOtherCalls();
        }
    }
}
