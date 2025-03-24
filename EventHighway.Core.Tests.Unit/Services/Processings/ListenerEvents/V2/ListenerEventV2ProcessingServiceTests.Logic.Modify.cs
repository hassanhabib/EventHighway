// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents.V2
{
    public partial class ListenerEventV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldModifyListenerEventV2Async()
        {
            // given
            ListenerEventV1 randomListenerEventV2 =
                CreateRandomListenerEventV2();

            ListenerEventV1 inputListenerEventV2 =
                randomListenerEventV2;

            ListenerEventV1 modifiedListenerEventV2 =
                inputListenerEventV2;

            ListenerEventV1 expectedListenerEventV2 =
                modifiedListenerEventV2.DeepClone();

            this.listenerEventV2ServiceMock.Setup(broker =>
                broker.ModifyListenerEventV2Async(
                    inputListenerEventV2))
                        .ReturnsAsync(modifiedListenerEventV2);

            // when
            ListenerEventV1 actualListenerEventV2 =
                await this.listenerEventV2ProcessingService
                    .ModifyListenerEventV2Async(
                        inputListenerEventV2);

            // then
            actualListenerEventV2.Should().BeEquivalentTo(
                expectedListenerEventV2);

            this.listenerEventV2ServiceMock.Verify(broker =>
                broker.ModifyListenerEventV2Async(
                    inputListenerEventV2),
                        Times.Once);

            this.listenerEventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
