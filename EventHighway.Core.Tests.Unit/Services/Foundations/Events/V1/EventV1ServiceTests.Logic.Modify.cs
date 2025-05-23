// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V1
{
    public partial class EventV1ServiceTests
    {
        [Fact]
        public async Task ShouldModifyEventV1Async()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            EventV1 randomEventV1 =
                CreateRandomEventV1(
                    dates: randomDateTimeOffset);

            EventV1 inputEventV1 = randomEventV1;
            EventV1 updatedEventV1 = inputEventV1;

            EventV1 expectedEventV1 =
                updatedEventV1.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateEventV1Async(inputEventV1))
                    .ReturnsAsync(updatedEventV1);

            // when
            EventV1 actualEventV1 =
                await this.eventV1Service
                    .ModifyEventV1Async(inputEventV1);

            // then
            actualEventV1.Should().BeEquivalentTo(
                expectedEventV1);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateEventV1Async(inputEventV1),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
