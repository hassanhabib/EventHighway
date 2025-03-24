// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Clients.ListenerEvents.V2
{
    public partial class ListenerEventV2sClientTests
    {
        [Fact]
        public async Task ShouldRemoveListenerEventV2ByIdAsync()
        {
            // given
            Guid randomListenerEventV2Id = GetRandomId();
            Guid inputListenerEventV2Id = randomListenerEventV2Id;

            ListenerEventV1 randomListenerEventV2 =
                CreateRandomListenerEventV2();

            ListenerEventV1 removedListenerEventV2 =
                randomListenerEventV2;

            ListenerEventV1 expectedListenerEventV2 =
                removedListenerEventV2.DeepClone();

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveListenerEventV2ByIdAsync(
                    inputListenerEventV2Id))
                        .ReturnsAsync(removedListenerEventV2);

            // when
            ListenerEventV1 actualListenerEventV2 =
                await this.listenerEventV2SClient
                    .RemoveListenerEventV2ByIdAsync(
                        inputListenerEventV2Id);

            // then
            actualListenerEventV2.Should()
                .BeEquivalentTo(expectedListenerEventV2);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveListenerEventV2ByIdAsync(
                    inputListenerEventV2Id),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
