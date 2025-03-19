// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldRemoveEventListenerV2ByIdAsync()
        {
            // given
            Guid randomListenerEventId = GetRandomId();
            Guid inputListenerEventId = randomListenerEventId;

            EventListenerV2 randomEventListenerV2 =
                CreateRandomEventListenerV2();

            EventListenerV2 removedEventListenerV2 =
                randomEventListenerV2;

            EventListenerV2 expectedEventListenerV2 =
                removedEventListenerV2.DeepClone();

            this.eventListenerV2ProcessingServiceMock.Setup(service =>
                service.RemoveEventListenerV2ByIdAsync(
                    inputListenerEventId))
                        .ReturnsAsync(removedEventListenerV2);

            // when
            EventListenerV2 actualEventListenerV2 =
                await this.eventListenerV2OrchestrationService
                    .RemoveEventListenerV2ByIdAsync(
                        inputListenerEventId);

            // then
            actualEventListenerV2.Should()
                .BeEquivalentTo(expectedEventListenerV2);

            this.eventListenerV2ProcessingServiceMock.Verify(service =>
                service.RemoveEventListenerV2ByIdAsync(
                    inputListenerEventId),
                        Times.Once);

            this.eventListenerV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.listenerEventV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock
                .VerifyNoOtherCalls();
        }
    }
}
