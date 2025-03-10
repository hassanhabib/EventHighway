// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventCalls.V2
{
    public partial class EventCallV2ProcessingServiceTests
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

            this.eventCallV2ServiceMock.Setup(service =>
                service.RunEventCallV2Async(
                    inputEventCallV2))
                        .ReturnsAsync(ranEventCallV2);

            // when
            EventCallV2 actualEventCallV2 =
                await this.eventCallV2ProcessingService
                    .RunEventCallV2Async(inputEventCallV2);

            // then
            actualEventCallV2.Should().BeEquivalentTo(
                expectedEventCallV2);

            this.eventCallV2ServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    inputEventCallV2),
                        Times.Once);

            this.eventCallV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
