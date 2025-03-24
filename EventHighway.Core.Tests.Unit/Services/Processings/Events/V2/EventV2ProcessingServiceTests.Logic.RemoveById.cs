// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using Moq;
using System.Threading.Tasks;
using System;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using Force.DeepCloner;
using FluentAssertions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.Events.V2
{
    public partial class EventV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRemoveEventV2ByIdAsync()
        {
            // given
            Guid randomEventV2Id = GetRandomId();
            Guid inputEventV2Id = randomEventV2Id;
            EventV1 randomEventV2 = CreateRandomEventV2();
            EventV1 removedEventV2 = randomEventV2;

            EventV1 expectedEventV2 =
                removedEventV2.DeepClone();

            this.eventV2ServiceMock.Setup(service =>
                service.RemoveEventV2ByIdAsync(
                    inputEventV2Id))
                        .ReturnsAsync(removedEventV2);

            // when
            EventV1 actualEventV2 =
                await this.eventV2ProcessingService
                    .RemoveEventV2ByIdAsync(
                        inputEventV2Id);

            // then
            actualEventV2.Should()
                .BeEquivalentTo(expectedEventV2);

            this.eventV2ServiceMock.Verify(service =>
                service.RemoveEventV2ByIdAsync(
                    inputEventV2Id),
                        Times.Once);

            this.eventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
