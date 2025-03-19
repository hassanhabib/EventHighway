// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.Events.V2
{
    public partial class EventV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldAddEventV2Async()
        {
            // given
            EventV2 randomEventV2 =
                CreateRandomEventV2();

            EventV2 inputEventV2 = randomEventV2;
            EventV2 addedEventV2 = inputEventV2;

            EventV2 expectedEventV2 =
                addedEventV2.DeepClone();

            this.eventV2ServiceMock.Setup(broker =>
                broker.AddEventV2Async(
                    inputEventV2))
                        .ReturnsAsync(addedEventV2);

            // when
            EventV2 actualEventV2 =
                await this.eventV2ProcessingService
                    .AddEventV2Async(
                        inputEventV2);

            // then
            actualEventV2.Should().BeEquivalentTo(
                expectedEventV2);

            this.eventV2ServiceMock.Verify(broker =>
                broker.AddEventV2Async(
                    inputEventV2),
                        Times.Once);

            this.eventV2ServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock
                .VerifyNoOtherCalls();
        }
    }
}
