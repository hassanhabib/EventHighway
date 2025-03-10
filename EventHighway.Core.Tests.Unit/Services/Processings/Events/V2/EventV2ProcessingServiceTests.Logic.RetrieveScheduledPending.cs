// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.Events.V2
{
    public partial class EventV2ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveScheduledPendingEventV2sAsync()
        {
            // given
            int randomDaysAgo = GetNegativeRandomNumber();

            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            DateTimeOffset retrievedDateTimeOffset =
                randomDateTimeOffset;

            DateTimeOffset scheduledDateTimeOffset =
                retrievedDateTimeOffset.AddDays(randomDaysAgo);

            List<EventV2> randomEventV2s =
                CreateRandomEventV2s(
                    dates: scheduledDateTimeOffset,
                    eventV2Type: EventV2Type.Scheduled)
                        .ToList();

            List<EventV2> randomOtherEventV2s =
                CreateRandomEventV2s().ToList();

            IQueryable<EventV2> retrievedEventV2s =
                randomEventV2s.Union(randomOtherEventV2s)
                    .AsQueryable();

            IQueryable<EventV2> expectedEventV2s =
                randomEventV2s.AsQueryable();

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
