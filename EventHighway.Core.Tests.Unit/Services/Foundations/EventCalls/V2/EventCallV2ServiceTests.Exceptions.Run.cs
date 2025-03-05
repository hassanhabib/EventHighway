// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.EventCall.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventCalls.V2
{
    public partial class EventCallV2ServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRunIfCriticalDependencyErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            EventCallV2 someEventCallV2 = CreateRandomEventCallV2();

            var failedEventCallV2ConfigurationException =
                new FailedEventCallV2ConfigurationException(
                    message: "Failed event call configuration error occurred, contact support.",
                    innerException: criticalDependencyException);

            var expectedEventCallV2DependencyException =
                new EventCallV2DependencyException(
                    message: "Event call dependency error occurred, contact support.",
                    innerException: failedEventCallV2ConfigurationException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                        .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<EventCallV2> addEventCallV2Task =
                this.eventCallV2Service.RunEventCallV2Async(someEventCallV2);

            EventCallV2DependencyException actualEventCallV2DependencyException =
                await Assert.ThrowsAsync<EventCallV2DependencyException>(
                    addEventCallV2Task.AsTask);

            // then
            actualEventCallV2DependencyException.Should()
                .BeEquivalentTo(expectedEventCallV2DependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventCallV2DependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
