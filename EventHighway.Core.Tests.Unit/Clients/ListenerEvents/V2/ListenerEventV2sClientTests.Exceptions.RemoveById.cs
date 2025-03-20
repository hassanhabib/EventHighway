// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.ListenerEvents.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.ListenerEvents.V2
{
    public partial class ListenerEventV2sClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();

            var expectedListenerEventV2ClientDependencyValidationException =
                new ListenerEventV2ClientDependencyValidationException(
                    message: "Listener event client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RemoveListenerEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<ListenerEventV2> removeListenerEventV2ByIdTask =
                this.listenerEventV2SClient.RemoveListenerEventV2ByIdAsync(someListenerEventV2Id);

            ListenerEventV2ClientDependencyValidationException
                actualListenerEventV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<ListenerEventV2ClientDependencyValidationException>(
                        removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ClientDependencyValidationException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RemoveListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
