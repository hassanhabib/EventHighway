// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.Events.V2
{
    public partial class EventV2OrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldSubmitEventV2Async()
        {
            // given
            EventV1 randomEventV2 =
                CreateRandomEventV2();

            EventV1 inputEventV2 = randomEventV2;
            EventV1 addedEventV2 = inputEventV2;

            EventV1 expectedEventV2 =
                addedEventV2.DeepClone();

            EventAddressV1 randomEventAddressV2 =
                CreateRandomEventAddressV2();

            EventAddressV1 retrievedEventAddressV2 =
                randomEventAddressV2;

            this.eventAddressV2ProcessingServiceMock.Setup(service =>
                service.RetrieveEventAddressV1ByIdAsync(
                    inputEventV2.EventAddressId))
                        .ReturnsAsync(retrievedEventAddressV2);

            this.eventV2ProcessingServiceMock.Setup(service =>
                service.AddEventV2Async(inputEventV2))
                    .ReturnsAsync(addedEventV2);

            // when
            EventV1 actualEventV2 =
                await this.eventV2OrchestrationService
                    .SubmitEventV2Async(
                        inputEventV2);

            // then
            actualEventV2.Should().BeEquivalentTo(
                expectedEventV2);

            this.eventAddressV2ProcessingServiceMock.Verify(service =>
                service.RetrieveEventAddressV1ByIdAsync(
                    inputEventV2.EventAddressId),
                        Times.Once);

            this.eventV2ProcessingServiceMock.Verify(broker =>
                broker.AddEventV2Async(inputEventV2),
                    Times.Once);

            this.eventAddressV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.eventV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.eventCallV2ProcessingServiceMock
                .VerifyNoOtherCalls();

            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
