// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.EventListeners.V2
{
    public partial class EventListenerV2sClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            Guid someEventListenerV2Id = GetRandomId();

            var expectedEventListenerV2ClientDependencyValidationException =
                new EventListenerV2ClientDependencyValidationException(
                    message: "Event listener client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveEventListenerV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventListenerV2> removeEventListenerV2ByIdTask =
                this.eventListenerV2SClient.RemoveEventListenerV2ByIdAsync(someEventListenerV2Id);

            EventListenerV2ClientDependencyValidationException
                actualEventListenerV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<EventListenerV2ClientDependencyValidationException>(
                        removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2ClientDependencyValidationException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveEventListenerV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
