// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Clients.ListenerEvents.V2
{
    public partial class ListenerEventV2sClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAllListenerEventV2sAsync()
        {
            // given
            IQueryable<ListenerEventV2> randomListenerEventV2s =
                CreateRandomListenerEventV2s();

            IQueryable<ListenerEventV2> retrievedListenerEventV2s =
                randomListenerEventV2s;

            IQueryable<ListenerEventV2> expectedListenerEventV2s =
                retrievedListenerEventV2s.DeepClone();

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveAllListenerEventV2sAsync())
                    .ReturnsAsync(retrievedListenerEventV2s);

            // when
            IQueryable<ListenerEventV2> actualListenerEventV2s =
                await this.listenerEventV2SClient
                    .RetrieveAllListenerEventV2sAsync();

            // then
            actualListenerEventV2s.Should().BeEquivalentTo(
                expectedListenerEventV2s);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveAllListenerEventV2sAsync(),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock
                .VerifyNoOtherCalls();
        }
    }
}
