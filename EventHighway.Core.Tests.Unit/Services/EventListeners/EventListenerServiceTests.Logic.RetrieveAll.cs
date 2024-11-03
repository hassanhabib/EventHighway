// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.EventListeners
{
    public partial class EventListenerServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllEventListenersAsync()
        {
            // given
            IQueryable<EventListener> randomEventListeners =
                CreateRandomEventListeners();

            IQueryable<EventListener> retrievedEventListeners =
                randomEventListeners;

            IQueryable<EventListener> expectedEventListeners =
                randomEventListeners.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEventListenersAsync())
                    .ReturnsAsync(retrievedEventListeners);

            // when
            IQueryable<EventListener> actualEventListeners =
                await this.eventListenerService.RetrieveAllEventListenersAsync();

            // then
            actualEventListeners.Should().BeEquivalentTo(
                expectedEventListeners);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEventListenersAsync(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
