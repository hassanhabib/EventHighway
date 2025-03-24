// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveScheduledPendingEventV2sAsync()
        {
            // given
            IQueryable<EventV1> randomEventV2s = CreateRandomEventV2s();
            IQueryable<EventV1> retrievedEventV2s = randomEventV2s;

            IQueryable<EventV1> expectedEventV2s = retrievedEventV2s
                .DeepClone();

            this.eventV2ProcessingServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV1sAsync())
                    .ReturnsAsync(retrievedEventV2s);

            // when
            IQueryable<EventV1> actualEventV2s =
                await this.eventV2OrchestrationService
                    .RetrieveScheduledPendingEventV2sAsync();

            // then
            actualEventV2s.Should().BeEquivalentTo(expectedEventV2s);

            this.eventV2ProcessingServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV1sAsync(),
                    Times.Once);

            this.eventV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventAddressV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallV2ProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
