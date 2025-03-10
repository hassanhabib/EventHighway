// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventCalls.V2
{
    public partial class EventCallV2ServiceTests
    {
        [Fact]
        public async Task ShouldRunEventCallV2Async()
        {
            // given
            EventCallV2 randomEventCallV2 =
                CreateRandomEventCallV2();

            EventCallV2 inputEventCallV2 =
                randomEventCallV2;

            string randomCallResponse =
                CreateRandomResponse();

            string postedCallResponse =
                randomCallResponse;

            EventCallV2 expectedEventCallV2 =
                inputEventCallV2.DeepClone();

            expectedEventCallV2.Response =
                postedCallResponse;

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    inputEventCallV2.Content,
                    inputEventCallV2.Endpoint,
                    inputEventCallV2.Secret))
                        .ReturnsAsync(postedCallResponse);

            // when
            EventCallV2 actualEventCallV2 =
                await this.eventCallV2Service
                    .RunEventCallV2Async(inputEventCallV2);

            // then
            actualEventCallV2.Should().BeEquivalentTo(
                expectedEventCallV2);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    inputEventCallV2.Content,
                    inputEventCallV2.Endpoint,
                    inputEventCallV2.Secret),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
