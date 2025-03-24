// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventListeners.V2
{
    public partial class EventListenerV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveEventListenerV2sByEventAddressIdAsync()
        {
            // given
            Guid randomEventAddressId = GetRandomId();
            Guid inputEventAddressId = randomEventAddressId;

            List<EventListenerV1> randomEventListenerV2s =
                CreateRandomEventListenerV2s(randomEventAddressId)
                    .ToList();

            List<EventListenerV1> randomOtherEventListenerV2s =
                CreateRandomEventListenerV2s()
                    .ToList();

            IQueryable<EventListenerV1> retrievedEventListenerV2s =
                randomEventListenerV2s.Union(randomOtherEventListenerV2s)
                    .AsQueryable();

            IQueryable<EventListenerV1> expectedEventListenerV2s =
                randomEventListenerV2s.AsQueryable();

            this.eventListenerV2ServiceMock.Setup(service =>
                service.RetrieveAllEventListenerV2sAsync())
                    .ReturnsAsync(retrievedEventListenerV2s);

            // when
            IQueryable<EventListenerV1> actualEventListenerV2s =
                await this.eventListenerV2ProcessingService
                    .RetrieveEventListenerV2sByEventAddressIdAsync(
                        inputEventAddressId);

            // then
            actualEventListenerV2s.Should()
                .BeEquivalentTo(expectedEventListenerV2s);

            this.eventListenerV2ServiceMock.Verify(service =>
                service.RetrieveAllEventListenerV2sAsync(),
                    Times.Once);

            this.eventListenerV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
