// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.Events.V1
{
    public partial class EventV1ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldModifyEventV1Async()
        {
            // given
            EventV1 randomEventV1 =
                CreateRandomEventV1();

            EventV1 inputEventV1 = randomEventV1;
            EventV1 modifiedEventV1 = inputEventV1;

            EventV1 expectedEventV1 =
                modifiedEventV1.DeepClone();

            this.eventV1ServiceMock.Setup(broker =>
                broker.ModifyEventV1Async(
                    inputEventV1))
                        .ReturnsAsync(modifiedEventV1);

            // when
            EventV1 actualEventV1 =
                await this.eventV1ProcessingService
                    .ModifyEventV1Async(
                        inputEventV1);

            // then
            actualEventV1.Should().BeEquivalentTo(
                expectedEventV1);

            this.eventV1ServiceMock.Verify(broker =>
                broker.ModifyEventV1Async(
                    inputEventV1),
                        Times.Once);

            this.eventV1ServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock
                .VerifyNoOtherCalls();
        }
    }
}
