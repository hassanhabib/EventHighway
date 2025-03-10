// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.ListenerEvents.V2;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationOnRetrieveScheduledPendingIfListenerEventV2IsNullAndLogItAsync()
        {
            // given
            IQueryable<EventV2> someEventV2s = CreateRandomEventV2s();
            IQueryable<EventListenerV2> someEventListenerV2s = CreateRandomEventListenerV2s();

            ListenerEventV2 nullListenerEventV2 = null;

            var nullListenerEventV2CoordinationException =
                new NullListenerEventV2CoordinationException(message: "Listener event is null.");

            var expectedEventListenerV2CoordinationValidationException =
                new EventV2CoordinationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: nullListenerEventV2CoordinationException);

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV2sAsync())
                    .ReturnsAsync(someEventV2s);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()))
                        .ReturnsAsync(someEventListenerV2s);

            this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                service.AddListenerEventV2Async(It.IsAny<ListenerEventV2>()))
                    .ReturnsAsync(nullListenerEventV2);

            // when
            ValueTask retrieveScheduledPendingEventV2sTask =
                this.eventV2CoordinationService
                    .FireScheduledPendingEventV2sAsync();

            EventV2CoordinationValidationException
                actualEventV2CoordinationValidationException =
                    await Assert.ThrowsAsync<EventV2CoordinationValidationException>(
                        retrieveScheduledPendingEventV2sTask.AsTask);

            // then
            actualEventV2CoordinationValidationException.Should().BeEquivalentTo(
                expectedEventListenerV2CoordinationValidationException);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV2sAsync(),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV2Async(It.IsAny<ListenerEventV2>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2CoordinationValidationException))),
                        Times.Once);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV2Async(It.IsAny<ListenerEventV2>()),
                    Times.Never);

            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
