// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllEventListenerV2sAsync()
        {
            // given
            IQueryable<EventListenerV2> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            IQueryable<EventListenerV2> retrievedEventListenerV2s =
                randomEventListenerV2s;

            IQueryable<EventListenerV2> expectedEventListenerV2s =
                randomEventListenerV2s.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEventListenerV2sAsync())
                    .ReturnsAsync(retrievedEventListenerV2s);

            // when
            IQueryable<EventListenerV2> actualEventListenerV2s =
                await this.eventListenerV2Service
                    .RetrieveAllEventListenerV2sAsync();

            // then
            actualEventListenerV2s.Should().BeEquivalentTo(
                expectedEventListenerV2s);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEventListenerV2sAsync(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
