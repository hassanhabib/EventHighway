// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.ListenerEvents.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.ListenerEvents.V2
{
    public partial class ListenerEventV2sClientTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfDependencyValidationErrorOccursAsync()
        {
            // given
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventListenerV2OrchestrationDependencyValidationException =
                new EventListenerV2OrchestrationDependencyValidationException(
                    someMessage,
                    someInnerException);

            var expectedListenerEventV2ClientDependencyValidationException =
                new ListenerEventV2ClientDependencyValidationException(
                    message: "Listener event client validation error occurred, fix the errors and try again.",

                    innerException: eventListenerV2OrchestrationDependencyValidationException
                        .InnerException as Xeption);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveAllListenerEventV2sAsync())
                    .ThrowsAsync(eventListenerV2OrchestrationDependencyValidationException);

            // when
            ValueTask<IQueryable<ListenerEventV2>> retrieveAllListenerEventV2sTask =
                this.listenerEventV2SClient.RetrieveAllListenerEventV2sAsync();

            ListenerEventV2ClientDependencyValidationException
                actualListenerEventV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<ListenerEventV2ClientDependencyValidationException>(
                        retrieveAllListenerEventV2sTask.AsTask);

            // then
            actualListenerEventV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2ClientDependencyValidationException);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveAllListenerEventV2sAsync(),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
