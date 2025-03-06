// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Models.Processings.ListenerEvents.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.ListenerEvents.V2
{
    public partial class ListenerEventV2ProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(ListenerEventV2ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption listenerEventV2ValidationException)
        {
            // given
            ListenerEventV2 someListenerEventV2 = CreateRandomListenerEventV2();

            var expectedListenerEventV2ProcessingDependencyValidationException =
                new ListenerEventV2ProcessingDependencyValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: listenerEventV2ValidationException.InnerException as Xeption);

            this.listenerEventV2ServiceMock.Setup(service =>
                service.AddListenerEventV2Async(It.IsAny<ListenerEventV2>()))
                    .ThrowsAsync(listenerEventV2ValidationException);

            // when
            ValueTask<ListenerEventV2> addListenerEventV2Task =
                this.listenerEventV2ProcessingService.AddListenerEventV2Async(someListenerEventV2);

            ListenerEventV2ProcessingDependencyValidationException
                actualListenerEventV2ProcessingDependencyValidationException =
                    await Assert.ThrowsAsync<ListenerEventV2ProcessingDependencyValidationException>(
                        addListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ProcessingDependencyValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ProcessingDependencyValidationException);

            this.listenerEventV2ServiceMock.Verify(service =>
                service.AddListenerEventV2Async(It.IsAny<ListenerEventV2>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ProcessingDependencyValidationException))),
            Times.Once);

            this.listenerEventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ListenerEventV2DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyExceptionOccursAndLogItAsync(
            Xeption listenerEventV2DependencyException)
        {
            // given
            ListenerEventV2 someListenerEventV2 = CreateRandomListenerEventV2();

            var expectedListenerEventV2ProcessingDependencyException =
                new ListenerEventV2ProcessingDependencyException(
                    message: "Listener event dependency error occurred, contact support.",
                    innerException: listenerEventV2DependencyException.InnerException as Xeption);

            this.listenerEventV2ServiceMock.Setup(service =>
                service.AddListenerEventV2Async(It.IsAny<ListenerEventV2>()))
                    .ThrowsAsync(listenerEventV2DependencyException);

            // when
            ValueTask<ListenerEventV2> addListenerEventV2Task =
                this.listenerEventV2ProcessingService.AddListenerEventV2Async(someListenerEventV2);

            ListenerEventV2ProcessingDependencyException
                actualListenerEventV2ProcessingDependencyException =
                    await Assert.ThrowsAsync<ListenerEventV2ProcessingDependencyException>(
                        addListenerEventV2Task.AsTask);

            // then
            actualListenerEventV2ProcessingDependencyException.Should()
                .BeEquivalentTo(expectedListenerEventV2ProcessingDependencyException);

            this.listenerEventV2ServiceMock.Verify(service =>
                service.AddListenerEventV2Async(It.IsAny<ListenerEventV2>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ProcessingDependencyException))),
                        Times.Once);

            this.listenerEventV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
