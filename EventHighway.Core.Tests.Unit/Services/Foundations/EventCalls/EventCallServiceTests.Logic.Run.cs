// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.EventCalls
{
    public partial class EventCallServiceTests
    {
        [Fact]
        public async Task ShouldRunEventCallAsync()
        {
            // Arrange
            EventCall randomEventCall =
                CreateRandomEventCall();

            EventCall inputEventCall =
                randomEventCall;

            string postCallResponse =
                CreateRandomResponse();

            EventCall expectedEventCall =
                inputEventCall.DeepClone();

            expectedEventCall.Response =
                postCallResponse;

            this.apiBrokerMock
                .Setup(broker => broker.PostAsync(
                    inputEventCall.Content,
                    inputEventCall.Endpoint,
                    inputEventCall.Secret))
                        .ReturnsAsync(postCallResponse);

            // Act
            EventCall actualEventCall =
                await this.eventCallService
                    .RunAsync(inputEventCall);

            // Assert
            actualEventCall.Should().BeEquivalentTo(
                expectedEventCall);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    inputEventCall.Content,
                    inputEventCall.Endpoint,
                    inputEventCall.Secret),
                        Times.Once());

            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
