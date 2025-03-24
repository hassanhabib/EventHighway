// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
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
        public async Task ShouldRetrieveEventListenerV2sByEventAddressIdAsync()
        {
            // given
            Guid randomEventAddressId = GetRandomId();
            Guid inputEventAddressId = randomEventAddressId;

            IQueryable<EventListenerV1> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            IQueryable<EventListenerV1> retrievedEventListenerV2s =
                randomEventListenerV2s;

            IQueryable<EventListenerV1> expectedEventListenerV2s =
                retrievedEventListenerV2s.DeepClone();

            this.eventListenerV2ProcessingServiceMock.Setup(service =>
                service.RetrieveEventListenerV1sByEventAddressIdAsync(
                    inputEventAddressId))
                        .ReturnsAsync(retrievedEventListenerV2s);

            // when
            IQueryable<EventListenerV1> actualEventListenerV2s =
                await this.eventListenerV2OrchestrationService
                    .RetrieveEventListenerV2sByEventAddressIdAsync(
                        inputEventAddressId);

            // then
            actualEventListenerV2s.Should()
                .BeEquivalentTo(expectedEventListenerV2s);

            this.eventListenerV2ProcessingServiceMock.Verify(service =>
                service.RetrieveEventListenerV1sByEventAddressIdAsync(
                    inputEventAddressId),
                        Times.Once);

            this.eventListenerV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.listenerEventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
