// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventListeners.V2
{
    public partial class EventListenerV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRemoveEventListenerV2ByIdAsync()
        {
            // given
            Guid randomListenerEventId = GetRandomId();
            Guid inputListenerEventId = randomListenerEventId;

            EventListenerV1 randomEventListenerV2 =
                CreateRandomEventListenerV2();

            EventListenerV1 removedEventListenerV2 =
                randomEventListenerV2;

            EventListenerV1 expectedEventListenerV2 =
                removedEventListenerV2.DeepClone();

            this.eventListenerV2ServiceMock.Setup(service =>
                service.RemoveEventListenerV1ByIdAsync(
                    inputListenerEventId))
                        .ReturnsAsync(removedEventListenerV2);

            // when
            EventListenerV1 actualEventListenerV2 =
                await this.eventListenerV2ProcessingService
                    .RemoveEventListenerV2ByIdAsync(
                        inputListenerEventId);

            // then
            actualEventListenerV2.Should()
                .BeEquivalentTo(expectedEventListenerV2);

            this.eventListenerV2ServiceMock.Verify(service =>
                service.RemoveEventListenerV1ByIdAsync(
                    inputListenerEventId),
                        Times.Once);

            this.eventListenerV2ServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock
                .VerifyNoOtherCalls();
        }
    }
}
