// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
        public async Task ShouldRemoveListenerEventV2ByIdAsync()
        {
            // given
            Guid randomListenerEventId = GetRandomId();
            Guid inputListenerEventId = randomListenerEventId;

            ListenerEventV1 randomListenerEventV2 =
                CreateRandomListenerEventV2();

            ListenerEventV1 removedListenerEventV2 =
                randomListenerEventV2;

            ListenerEventV1 expectedListenerEventV2 =
                removedListenerEventV2.DeepClone();

            this.listenerEventV2ServiceMock.Setup(service =>
                service.RemoveListenerEventV2ByIdAsync(
                    inputListenerEventId))
                        .ReturnsAsync(removedListenerEventV2);

            // when
            ListenerEventV1 actualListenerEventV2 =
                await this.listenerEventV2ProcessingService
                    .RemoveListenerEventV2ByIdAsync(
                        inputListenerEventId);

            // then
            actualListenerEventV2.Should()
                .BeEquivalentTo(expectedListenerEventV2);

            this.listenerEventV2ServiceMock.Verify(service =>
                service.RemoveListenerEventV2ByIdAsync(
                    inputListenerEventId),
                        Times.Once);

            this.listenerEventV2ServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock
                .VerifyNoOtherCalls();
        }
    }
}
