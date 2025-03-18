// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using Moq;
using System.Threading.Tasks;
using System;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
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
            Guid randomListenerEventId = GetRandomId();
            Guid inputListenerEventId = randomListenerEventId;
            EventV2 randomEventV2 = CreateRandomEventV2();
            EventV2 removedEventV2 = randomEventV2;

            EventV2 expectedEventV2 =
                removedEventV2.DeepClone();

            this.eventV2ServiceMock.Setup(service =>
                service.RemoveEventV2ByIdAsync(
                    inputListenerEventId))
                        .ReturnsAsync(removedEventV2);

            // when
            EventV2 actualEventV2 =
                await this.eventV2ProcessingService
                    .RemoveEventV2ByIdAsync(
                        inputListenerEventId);

            // then
            actualEventV2.Should()
                .BeEquivalentTo(expectedEventV2);

            this.eventV2ServiceMock.Verify(service =>
                service.RemoveEventV2ByIdAsync(
                    inputListenerEventId),
                        Times.Once);

            this.eventV2ServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock
                .VerifyNoOtherCalls();
        }
    }
}
