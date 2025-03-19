// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Processings.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventListeners.V2
{
    public partial class EventListenerV2ProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveByEventAddressIdIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            Guid someEventAddressId = GetRandomId();

            var expectedEventListenerV2ProcessingDependencyException =
                new EventListenerV2ProcessingDependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption);

            this.eventListenerV2ServiceMock.Setup(service =>
                service.RetrieveAllEventListenerV2sAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<IQueryable<EventListenerV2>> retrieveEventListenerV2sByEventAddressIdTask =
                this.eventListenerV2ProcessingService.RetrieveEventListenerV2sByEventAddressIdAsync(
                    someEventAddressId);

            EventListenerV2ProcessingDependencyException actualEventListenerV2ProcessingDependencyException =
                await Assert.ThrowsAsync<EventListenerV2ProcessingDependencyException>(
                    retrieveEventListenerV2sByEventAddressIdTask.AsTask);

            // then
            actualEventListenerV2ProcessingDependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2ProcessingDependencyException);

            this.eventListenerV2ServiceMock.Verify(service =>
                service.RetrieveAllEventListenerV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ProcessingDependencyException))),
                        Times.Once);

            this.eventListenerV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnOnRetrieveByEventAddressIdIfExceptionOccursAndLogItAsync()
        {
            // given
            Guid someEventAddressId = GetRandomId();
            var serviceException = new Exception();

            var failedEventListenerV2ProcessingServiceException =
                new FailedEventListenerV2ProcessingServiceException(
                    message: "Failed event listener service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventListenerV2ProcessingServiceException =
                new EventListenerV2ProcessingServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: failedEventListenerV2ProcessingServiceException);

            this.eventListenerV2ServiceMock.Setup(service =>
                service.RetrieveAllEventListenerV2sAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<IQueryable<EventListenerV2>> retrieveEventListenerV2sByEventAddressIdTask =
                this.eventListenerV2ProcessingService.RetrieveEventListenerV2sByEventAddressIdAsync(
                    someEventAddressId);

            EventListenerV2ProcessingServiceException actualEventListenerV2ProcessingServiceException =
                await Assert.ThrowsAsync<EventListenerV2ProcessingServiceException>(
                    retrieveEventListenerV2sByEventAddressIdTask.AsTask);

            // then
            actualEventListenerV2ProcessingServiceException.Should()
                .BeEquivalentTo(expectedEventListenerV2ProcessingServiceException);

            this.eventListenerV2ServiceMock.Verify(service =>
                service.RetrieveAllEventListenerV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ProcessingServiceException))),
                        Times.Once);

            this.eventListenerV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
