// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.ListenerEvents.V2
{
    public partial class ListenerEventV2ServiceTests
    {
        [Fact]
        public async Task ShouldAddListenerEventV2Async()
        {
            // given
            ListenerEventV2 randomListenerEventV2 =
                CreateRandomListenerEventV2();

            ListenerEventV2 inputListenerEventV2 =
                randomListenerEventV2;

            ListenerEventV2 storageListenerEventV2 =
                inputListenerEventV2;

            ListenerEventV2 expectedListenerEventV2 =
                storageListenerEventV2.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertListenerEventV2Async(
                    inputListenerEventV2))
                        .ReturnsAsync(storageListenerEventV2);

            // when
            ListenerEventV2 actualListenerEventV2 =
                await this.listenerEventV2Service
                    .AddListenerEventV2Async(
                        inputListenerEventV2);

            // then
            actualListenerEventV2.Should().BeEquivalentTo(
                expectedListenerEventV2);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertListenerEventV2Async(
                    inputListenerEventV2),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
