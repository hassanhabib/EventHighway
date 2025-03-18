// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

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
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            EventListenerV2 someEventListenerV2 = CreateRandomEventListenerV2();

            var expectedEventListenerV2ProcessingDependencyValidationException =
                new EventListenerV2ProcessingDependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventListenerV2ServiceMock.Setup(service =>
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventListenerV2> addEventListenerV2Task =
                this.eventListenerV2ProcessingService.AddEventListenerV2Async(someEventListenerV2);

            EventListenerV2ProcessingDependencyValidationException
                actualEventListenerV2ProcessingDependencyValidationException =
                    await Assert.ThrowsAsync<EventListenerV2ProcessingDependencyValidationException>(
                        addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ProcessingDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2ProcessingDependencyValidationException);

            this.eventListenerV2ServiceMock.Verify(service =>
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ProcessingDependencyValidationException))),
                        Times.Once);

            this.eventListenerV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyExceptionOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            EventListenerV2 someEventListenerV2 = CreateRandomEventListenerV2();

            var expectedEventListenerV2ProcessingDependencyException =
                new EventListenerV2ProcessingDependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption);

            this.eventListenerV2ServiceMock.Setup(service =>
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<EventListenerV2> addEventListenerV2Task =
                this.eventListenerV2ProcessingService.AddEventListenerV2Async(someEventListenerV2);

            EventListenerV2ProcessingDependencyException
                actualEventListenerV2ProcessingDependencyException =
                    await Assert.ThrowsAsync<EventListenerV2ProcessingDependencyException>(
                        addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ProcessingDependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2ProcessingDependencyException);

            this.eventListenerV2ServiceMock.Verify(service =>
                service.AddEventListenerV2Async(It.IsAny<EventListenerV2>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ProcessingDependencyException))),
                        Times.Once);

            this.eventListenerV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
