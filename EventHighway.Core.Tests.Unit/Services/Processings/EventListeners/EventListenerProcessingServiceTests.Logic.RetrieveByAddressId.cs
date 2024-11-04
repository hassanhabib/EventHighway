// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventListeners
{
    public partial class EventListenerProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveEventListenersByAddressIdAsync()
        {
            // given
            Guid randomAddressId = Guid.NewGuid();
            Guid inputAddressId = randomAddressId;

            IQueryable<EventListener> randomEventListeners =
                CreateRandomEventListeners(randomAddressId);

            IQueryable<EventListener> randomOtherEventListeners =
                CreateRandomEventListeners();

            IQueryable<EventListener> retrievedEventListeners =
                randomEventListeners.Union(randomOtherEventListeners);

            IQueryable<EventListener> expectedEventListeners =
                randomEventListeners;

            this.eventListenerServiceMock.Setup(service =>
                service.RetrieveAllEventListenersAsync())
                    .ReturnsAsync(retrievedEventListeners);

            // when
            IQueryable<EventListener> actualEventListeners =
                await this.eventListenerProcessingService
                    .RetrieveEventListenersByAddressIdAsync(inputAddressId);

            // then
            actualEventListeners.Should().BeEquivalentTo(expectedEventListeners);

            this.eventListenerServiceMock.Verify(service =>
                service.RetrieveAllEventListenersAsync(),
                    Times.Once);

            this.eventListenerServiceMock.VerifyNoOtherCalls();
        }
    }
}
