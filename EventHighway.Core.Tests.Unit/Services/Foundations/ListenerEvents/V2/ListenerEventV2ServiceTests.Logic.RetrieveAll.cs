// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.ListenerEvents.V2
{
    public partial class ListenerEventV2ServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllListenerEventV2sAsync()
        {
            // given
            IQueryable<ListenerEventV1> randomListenerEventV2s =
                CreateRandomListenerEventV2s();

            IQueryable<ListenerEventV1> retrievedListenerEventV2s =
                randomListenerEventV2s;

            IQueryable<ListenerEventV1> expectedListenerEventV2s =
                randomListenerEventV2s.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllListenerEventV2sAsync())
                    .ReturnsAsync(retrievedListenerEventV2s);

            // when
            IQueryable<ListenerEventV1> actualListenerEventV2s =
                await this.listenerEventV2Service
                    .RetrieveAllListenerEventV2sAsync();

            // then
            actualListenerEventV2s.Should().BeEquivalentTo(
                expectedListenerEventV2s);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllListenerEventV2sAsync(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
