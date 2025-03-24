// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        [Fact]
        public async Task ShouldSubmitEventV2Async()
        {
            // given 
            EventV1 randomEventV2 = CreateRandomEventV2();
            EventV1 inputEventV2 = randomEventV2;
            EventV1 submittedEventV2 = inputEventV2;

            EventV1 expectedEventV2 = 
                submittedEventV2.DeepClone();

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.SubmitEventV2Async(inputEventV2))
                    .ReturnsAsync(submittedEventV2);

            // when
            EventV1 actualEventV2 =
                await this.eventV2SClient
                    .SubmitEventV2Async(inputEventV2);

            // then
            actualEventV2.Should()
                .BeEquivalentTo(expectedEventV2);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.SubmitEventV2Async(inputEventV2),
                    Times.Once);

            this.eventV2CoordinationServiceMock
                .VerifyNoOtherCalls();
        }
    }
}
