// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldRunEventCallV2Async()
        {
            // given
            EventCallV2 randomEventCallV2 =
                CreateRandomEventCallV2();

            EventCallV2 inputEventCallV2 =
                randomEventCallV2;

            EventCallV2 ranEventCallV2 =
                inputEventCallV2;

            EventCallV2 expectedEventCallV2 =
                inputEventCallV2.DeepClone();

            this.eventCallV2ProcessingServiceMock.Setup(service =>
                service.RunEventCallV2Async(
                    inputEventCallV2))
                        .ReturnsAsync(ranEventCallV2);

            // when
            EventCallV2 actualEventCallV2 =
                await this.eventV2OrchestrationService
                    .RunEventCallV2Async(inputEventCallV2);

            // then
            actualEventCallV2.Should().BeEquivalentTo(
                expectedEventCallV2);

            this.eventCallV2ProcessingServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    inputEventCallV2),
                        Times.Once);

            this.eventCallV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.eventV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.eventAddressV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
