// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventCalls
{
    public partial class EventCallProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRunEventCallAsync()
        {
            // Arrange
            EventCall randomEventCall =
                CreateRandomEventCall();

            EventCall inputEventCall =
                randomEventCall;

            EventCall ranEventCall =
                inputEventCall;

            EventCall expectedEventCall =
                inputEventCall.DeepClone();

            this.eventCallServiceMock.Setup(service =>
                service.RunAsync(inputEventCall))
                    .ReturnsAsync(ranEventCall);

            // Act
            EventCall actualEventCall =
                await this.eventCallProcessingService
                    .RunAsync(inputEventCall);

            // Assert
            actualEventCall.Should().BeEquivalentTo(
                expectedEventCall);

            this.eventCallServiceMock.Verify(service =>
                service.RunAsync(inputEventCall),
                    Times.Once);

            this.eventCallServiceMock.VerifyNoOtherCalls();
        }
    }
}
