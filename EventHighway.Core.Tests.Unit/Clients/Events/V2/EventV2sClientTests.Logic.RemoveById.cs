// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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
        public async Task ShouldRemoveEventV2ByIdAsync()
        {
            // given 
            Guid randomEventV2Id = GetRandomId();
            Guid inputEventV2Id = randomEventV2Id;
            EventV1 randomEventV2 = CreateRandomEventV2();
            EventV1 removedEventV2 = randomEventV2;

            EventV1 expectedEventV2 =
                removedEventV2.DeepClone();

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.RemoveEventV1ByIdAsync(inputEventV2Id))
                    .ReturnsAsync(removedEventV2);

            // when
            EventV1 actualEventV2 =
                await this.eventV2SClient
                    .RemoveEventV2ByIdAsync(inputEventV2Id);

            // then
            actualEventV2.Should()
                .BeEquivalentTo(expectedEventV2);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.RemoveEventV1ByIdAsync(inputEventV2Id),
                    Times.Once);

            this.eventV2CoordinationServiceMock
                .VerifyNoOtherCalls();
        }
    }
}
