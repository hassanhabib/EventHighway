// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Events.V2;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.Events.V2
{
    public partial class EventV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveScheduledPendingEventV2sAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            DateTimeOffset retrievedDateTimeOffset =
                randomDateTimeOffset;

            IQueryable<EventV2> randomEventV2s = CreateRandomEventV2s();
            IQueryable<EventV2> retrievedEventV2s = randomEventV2s;

            IQueryable<EventV2> expectedEventV2s =
                retrievedEventV2s.Where(eventV2 =>
                    eventV2.Type == EventV2Type.Scheduled &&
                    eventV2.ScheduledDate > retrievedDateTimeOffset)
                        .DeepClone();

            this.eventV2ServiceMock.Setup(service =>
                service.RetrieveAllEventV2sAsync())
                    .ReturnsAsync(retrievedEventV2s);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(retrievedDateTimeOffset);

            // when
            IQueryable<EventV2> actualEventV2s =
                await this.eventV2ProcessingService
                    .RetrieveScheduledPendingEventV2sAsync();

            // then
            actualEventV2s.Should().BeEquivalentTo(expectedEventV2s);

            this.eventV2ServiceMock.Verify(service =>
                service.RetrieveAllEventV2sAsync(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.eventV2ServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
