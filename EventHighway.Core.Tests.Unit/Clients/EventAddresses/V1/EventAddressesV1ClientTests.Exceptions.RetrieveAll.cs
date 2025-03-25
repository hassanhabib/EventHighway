// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Clients.EventAddresses.V1.Exceptions;
using EventHighway.Core.Models.Clients.ListenerEvents.V1.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.EventAddresses.V1
{
    public partial class EventAddressesV1ClientTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfDependencyErrorOccursAsync()
        {
            // given
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventListenerV1OrchestrationDependencyException =
                new EventListenerV1OrchestrationDependencyException(
                    someMessage,
                    someInnerException);

            var expectedEventAddressV1ClientDependencyException =
                new EventAddressV1ClientDependencyException(
                    message: "Event address client dependency error occurred, contact support.",

                    innerException: eventListenerV1OrchestrationDependencyException
                        .InnerException as Xeption);

            this.eventAddressV1ServiceMock.Setup(service =>
                service.RetrieveAllEventAddressV1sAsync())
                    .ThrowsAsync(eventListenerV1OrchestrationDependencyException);

            // when
            ValueTask<IQueryable<EventAddressV1>> retrieveAllEventAddressV1sTask =
                this.eventAddressV1sClient.RetrieveAllEventAddressV1sAsync();

            EventAddressV1ClientDependencyException actualEventAddressV1ClientDependencyException =
                await Assert.ThrowsAsync<EventAddressV1ClientDependencyException>(
                    retrieveAllEventAddressV1sTask.AsTask);

            // then
            actualEventAddressV1ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventAddressV1ClientDependencyException);

            this.eventAddressV1ServiceMock.Verify(service =>
                service.RetrieveAllEventAddressV1sAsync(),
                    Times.Once);

            this.eventAddressV1ServiceMock.VerifyNoOtherCalls();
        }
    }
}
