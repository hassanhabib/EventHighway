﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners.V2
{
    public partial class EventListenerV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldAddEventListenerV2Async()
        {
            // given
            EventListenerV1 randomEventListenerV2 =
                CreateRandomEventListenerV2();

            EventListenerV1 inputEventListenerV2 =
                randomEventListenerV2;

            EventListenerV1 addedEventListenerV2 =
                inputEventListenerV2;

            EventListenerV1 expectedEventListenerV2 =
                addedEventListenerV2.DeepClone();

            this.eventListenerV2ProcessingServiceMock.Setup(broker =>
                broker.AddEventListenerV1Async(
                    inputEventListenerV2))
                        .ReturnsAsync(addedEventListenerV2);

            // when
            EventListenerV1 actualEventListenerV2 =
                await this.eventListenerV2OrchestrationService
                    .AddEventListenerV2Async(
                        inputEventListenerV2);

            // then
            actualEventListenerV2.Should().BeEquivalentTo(
                expectedEventListenerV2);

            this.eventListenerV2ProcessingServiceMock.Verify(broker =>
                broker.AddEventListenerV1Async(
                    inputEventListenerV2),
                        Times.Once);

            this.eventListenerV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.listenerEventV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
