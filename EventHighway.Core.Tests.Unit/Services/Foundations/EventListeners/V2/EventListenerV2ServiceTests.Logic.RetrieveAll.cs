// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllEventListenerV2sAsync()
        {
            // given
            IQueryable<EventListenerV1> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            IQueryable<EventListenerV1> retrievedEventListenerV2s =
                randomEventListenerV2s;

            IQueryable<EventListenerV1> expectedEventListenerV2s =
                randomEventListenerV2s.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEventListenerV1sAsync())
                    .ReturnsAsync(retrievedEventListenerV2s);

            // when
            IQueryable<EventListenerV1> actualEventListenerV2s =
                await this.eventListenerV2Service
                    .RetrieveAllEventListenerV2sAsync();

            // then
            actualEventListenerV2s.Should().BeEquivalentTo(
                expectedEventListenerV2s);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEventListenerV1sAsync(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
