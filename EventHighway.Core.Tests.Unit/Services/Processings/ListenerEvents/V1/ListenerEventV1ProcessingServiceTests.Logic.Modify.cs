﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents.V1
{
    public partial class ListenerEventV1ProcessingServiceTests
    {
        [Fact]
        public async Task ShouldModifyListenerEventV1Async()
        {
            // given
            ListenerEventV1 randomListenerEventV1 =
                CreateRandomListenerEventV1();

            ListenerEventV1 inputListenerEventV1 =
                randomListenerEventV1;

            ListenerEventV1 modifiedListenerEventV1 =
                inputListenerEventV1;

            ListenerEventV1 expectedListenerEventV1 =
                modifiedListenerEventV1.DeepClone();

            this.listenerEventV1ServiceMock.Setup(broker =>
                broker.ModifyListenerEventV1Async(
                    inputListenerEventV1))
                        .ReturnsAsync(modifiedListenerEventV1);

            // when
            ListenerEventV1 actualListenerEventV1 =
                await this.listenerEventV1ProcessingService
                    .ModifyListenerEventV1Async(
                        inputListenerEventV1);

            // then
            actualListenerEventV1.Should().BeEquivalentTo(
                expectedListenerEventV1);

            this.listenerEventV1ServiceMock.Verify(broker =>
                broker.ModifyListenerEventV1Async(
                    inputListenerEventV1),
                        Times.Once);

            this.listenerEventV1ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
