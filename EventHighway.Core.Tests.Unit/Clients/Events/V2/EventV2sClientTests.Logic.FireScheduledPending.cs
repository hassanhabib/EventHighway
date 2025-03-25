// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;

namespace EventHighway.Core.Tests.Unit.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        [Fact]
        public async Task ShouldFireScheduledPendingEventV2sAsync()
        {
            // given . when
            await this.eventV2SClient
                .FireScheduledPendingEventV2sAsync();

            // then
            this.eventV2CoordinationServiceMock.Verify(service =>
                service.FireScheduledPendingEventV1sAsync(),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }
    }
}
