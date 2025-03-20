// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Clients.EventListeners.V2
{
    public partial class EventListenerV2sClientTests
    {
        [Fact]
        public async Task ShouldRegisterEventListenerV2Async()
        {
            // given
            EventListenerV2 randomEventListenerV2 =
                CreateRandomEventListenerV2();

            EventListenerV2 inputEventListenerV2 =
                randomEventListenerV2;

            EventListenerV2 registeredEventListenerV2 =
                inputEventListenerV2;

            EventListenerV2 expectedEventListenerV2 =
                registeredEventListenerV2.DeepClone();

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.AddEventListenerV2Async(
                    inputEventListenerV2))
                        .ReturnsAsync(registeredEventListenerV2);

            // when
            EventListenerV2 actualEventListenerV2 =
                await this.eventListenerV2SClient
                    .RegisterEventListenerV2Async(
                        inputEventListenerV2);

            // then
            actualEventListenerV2.Should()
                .BeEquivalentTo(expectedEventListenerV2);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddEventListenerV2Async(
                    inputEventListenerV2),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock
                .VerifyNoOtherCalls();
        }
    }
}
