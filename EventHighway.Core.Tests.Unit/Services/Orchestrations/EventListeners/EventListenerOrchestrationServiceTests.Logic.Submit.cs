// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall;
using EventHighway.Core.Models.EventListeners;
using EventHighway.Core.Models.Events;
using EventHighway.Core.Models.ListenerEvents;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners
{
    public partial class EventListenerOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldSubmitEventToListenersAsync()
        {
            // given
            Event randomEvent = CreateRandomEvent();
            Event inputEvent = randomEvent;
            Event expectedEvent = inputEvent.DeepClone();
            Guid eventAddressId = inputEvent.EventAddressId;

            IQueryable<EventListener> randomEventListeners =
                CreateRandomEventListeners();

            IQueryable<EventListener> retrievedEventListeners =
                randomEventListeners;

            DateTimeOffset randomDate = GetRandomDateTime();
            DateTimeOffset updatedDate = randomDate;

            List<ListenerEvent> expectedListenerEvents =
                retrievedEventListeners.Select(retrievedEventListener =>
                    new ListenerEvent
                    {
                        EventListenerId = retrievedEventListener.Id,
                        EventId = inputEvent.Id,
                        CreatedDate = inputEvent.CreatedDate,
                        UpdatedDate = inputEvent.UpdatedDate
                    }).ToList();

            List<EventCall> expectedCallEvents =
                retrievedEventListeners.Select(retrievedEventListener =>
                    new EventCall
                    {
                        Endpoint = retrievedEventListener.Endpoint,
                        Content = inputEvent.Content,
                        Response = null
                    }).ToList();

            var ranEventCalls = new List<EventCall>();

            this.eventListenerProcessingServiceMock.Setup(service =>
                service.RetrieveEventListenersByAddressIdAsync(eventAddressId))
                    .ReturnsAsync(retrievedEventListeners);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(updatedDate);

            foreach (ListenerEvent expectedListenerEvent in expectedListenerEvents)
            {
                this.listenerEventProcessingServiceMock.Setup(service =>
                    service.AddListenerEventAsync(
                        It.Is(SameListenerEventAs(expectedListenerEvent))))
                            .ReturnsAsync(expectedListenerEvent);
            }

            foreach (EventCall expectedCallEvent in expectedCallEvents)
            {
                var ranEventCall = new EventCall
                {
                    Endpoint = expectedCallEvent.Endpoint,
                    Content = expectedCallEvent.Content,
                    Response = CreateRandomEventCallResponse()
                };

                this.eventCallProcessingServiceMock.Setup(service =>
                    service.RunAsync(
                        It.Is(SameEventCallAs(expectedCallEvent))))
                            .ReturnsAsync(ranEventCall);

                ranEventCalls.Add(ranEventCall);
            }

            for (int i = 0; i < retrievedEventListeners.Count(); i++)
            {
                expectedListenerEvents[i].UpdatedDate = updatedDate;
                expectedListenerEvents[i].Status = ListenerEventStatus.Completed;
                expectedListenerEvents[i].Response = ranEventCalls[i].Response;

                this.listenerEventProcessingServiceMock.Setup(service =>
                    service.ModifyListenerEventAsync(
                        It.Is(SameListenerEventAs(expectedListenerEvents[i]))))
                            .ReturnsAsync(expectedListenerEvents[i]);
            }

            // when
            Event actualEvent = 
                await this.eventListenerOrchestrationService
                    .SubmitEventToListenersAsync(inputEvent);

            // then
            actualEvent.Should().BeEquivalentTo(expectedEvent);

            this.eventListenerProcessingServiceMock.Verify(service =>
                service.RetrieveEventListenersByAddressIdAsync(eventAddressId),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Exactly(retrievedEventListeners.Count()));

            foreach (ListenerEvent expectedListenerEvent in expectedListenerEvents)
            {
                this.listenerEventProcessingServiceMock.Verify(service =>
                    service.AddListenerEventAsync(
                        It.Is(SameListenerEventAs(expectedListenerEvent))),
                            Times.Once);
            }

            foreach (EventCall expectedCallEvent in expectedCallEvents)
            {
                this.eventCallProcessingServiceMock.Verify(service =>
                    service.RunAsync(
                        It.Is(SameEventCallAs(expectedCallEvent))),
                            Times.Once);
            }

            for (int i = 0; i < retrievedEventListeners.Count(); i++)
            {
                this.listenerEventProcessingServiceMock.Verify(service =>
                    service.ModifyListenerEventAsync(
                        It.Is(SameListenerEventAs(expectedListenerEvents[i]))),
                            Times.Once);
            }

            this.eventListenerProcessingServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.listenerEventProcessingServiceMock.VerifyNoOtherCalls();
            this.eventCallProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
