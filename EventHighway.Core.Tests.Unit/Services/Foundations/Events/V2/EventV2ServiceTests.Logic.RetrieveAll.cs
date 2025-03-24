// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V2
{
    public partial class EventV2ServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllEventV2sAsync()
        {
            // given
            IQueryable<EventV1> randomEventV2s =
                CreateRandomEventV2s();

            IQueryable<EventV1> retrievedEventV2s =
                randomEventV2s;

            IQueryable<EventV1> expectedEventV2s =
                retrievedEventV2s.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEventV1sAsync())
                    .ReturnsAsync(retrievedEventV2s);

            // when
            IQueryable<EventV1> actualEventV2s =
                await this.eventV2Service
                    .RetrieveAllEventV2sAsync();

            // then
            actualEventV2s.Should().BeEquivalentTo(
                expectedEventV2s);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEventV1sAsync(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
