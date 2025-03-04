// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V2
{
    public partial class EventV2ServiceTests
    {
        [Fact]
        public async Task ShouldAddEventV2Async()
        {
            // given
            EventV2 randomEventV2 =
                CreateRandomEventV2();

            EventV2 inputEventV2 = randomEventV2;
            EventV2 insertedEventV2 = inputEventV2;

            EventV2 expectedEventV2 =
                insertedEventV2.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventV2Async(inputEventV2))
                    .ReturnsAsync(insertedEventV2);

            // when
            EventV2 actualEventV2 =
                await this.eventV2Service
                    .AddEventV2Async(inputEventV2);

            // then
            actualEventV2.Should().BeEquivalentTo(
                expectedEventV2);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV2Async(inputEventV2),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
