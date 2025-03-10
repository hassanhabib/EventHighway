// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.ListenerEvents.V2;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        [Fact]
        public async Task ShouldFireScheduledPendingEventV2sAsync()
        {
            // given
            IQueryable<EventV2> randomEventV2s = CreateRandomEventV2s();
            IQueryable<EventV2> retrievedEventV2s = randomEventV2s;

            IQueryable<EventListenerV2> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            IQueryable<EventListenerV2> retrievedEventListenerV2s =
                randomEventListenerV2s;

            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            DateTimeOffset retrievedDateTimeOffset = randomDateTimeOffset;

            List<ListenerEventV2> expectedListenerEventV2s =
                retrievedEventV2s.SelectMany(eventV2 =>
                    retrievedEventListenerV2s.Select(eventListenerV2 =>
                        new ListenerEventV2
                        {
                            EventListenerId = eventListenerV2.Id,
                            EventId = eventV2.Id,
                            Status = ListenerEventV2Status.Pending,
                            EventAddressId = eventV2.EventAddressId,
                            CreatedDate = eventV2.CreatedDate,
                            UpdatedDate = eventV2.UpdatedDate
                        })).ToList();


            List<ListenerEventV2> expectedListenerEventV2sOnModify =
                expectedListenerEventV2s.DeepClone();

            List<EventCallV2> expectedCallEventV2s =
                retrievedEventV2s.SelectMany(eventV2 =>
                    retrievedEventListenerV2s.Select(
                        retrievedEventListenerV2 =>
                            new EventCallV2
                            {
                                Endpoint = retrievedEventListenerV2.Endpoint,
                                Content = eventV2.Content,
                                Secret = retrievedEventListenerV2.HeaderSecret,
                            })).ToList();

            var ranEventCallV2s = new List<EventCallV2>();

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV2sAsync())
                    .ReturnsAsync(retrievedEventV2s);

            foreach (EventV2 eventV2 in retrievedEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId))
                            .ReturnsAsync(retrievedEventListenerV2s);
            }

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(retrievedDateTimeOffset);

            foreach (ListenerEventV2 expectedListenerEventV2 in expectedListenerEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.AddListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))))
                            .ReturnsAsync(expectedListenerEventV2.DeepClone());
            }

            foreach (EventCallV2 expectedCallEventV2 in expectedCallEventV2s)
            {
                var ranEventCall = new EventCallV2
                {
                    Endpoint = expectedCallEventV2.Endpoint,
                    Content = expectedCallEventV2.Content,
                    Response = GetRandomString()
                };

                this.eventV2OrchestrationServiceMock.Setup(service =>
                    service.RunEventCallV2Async(
                        It.Is(SameEventCallAs(expectedCallEventV2))))
                            .ReturnsAsync(ranEventCall);

                ranEventCallV2s.Add(ranEventCall);
            }

            for (int i = 0; i < expectedListenerEventV2s.Count(); i++)
            {
                expectedListenerEventV2sOnModify[i].UpdatedDate = retrievedDateTimeOffset;
                expectedListenerEventV2sOnModify[i].Status = ListenerEventV2Status.Success;
                expectedListenerEventV2sOnModify[i].Response = ranEventCallV2s[i].Response;

                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.ModifyListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2sOnModify[i]))))
                            .ReturnsAsync(expectedListenerEventV2sOnModify[i]);
            }

            // when
            await this.eventV2CoordinationService
                .FireScheduledPendingEventV2sAsync();

            // then
            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV2sAsync(),
                    Times.Once);

            foreach (EventV2 eventV2 in retrievedEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId),
                            Times.Once);
            }

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Exactly(callCount: expectedListenerEventV2s.Count()));

            foreach (ListenerEventV2 expectedListenerEventV2 in expectedListenerEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.AddListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))),
                            Times.Once);
            }

            foreach (EventCallV2 expectedCallEventV2 in expectedCallEventV2s)
            {
                this.eventV2OrchestrationServiceMock.Verify(service =>
                    service.RunEventCallV2Async(
                        It.Is(SameEventCallAs(expectedCallEventV2))),
                            Times.Once);
            }

            foreach (ListenerEventV2 expectedListenerEventV2 in expectedListenerEventV2sOnModify)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.ModifyListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))),
                            Times.Once);
            }

            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRecordFailuresOnFireScheduledPendingEventV2sAsync()
        {
            // given
            IQueryable<EventV2> randomEventV2s = CreateRandomEventV2s();
            IQueryable<EventV2> retrievedEventV2s = randomEventV2s;

            IQueryable<EventListenerV2> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            IQueryable<EventListenerV2> retrievedEventListenerV2s =
                randomEventListenerV2s;

            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            DateTimeOffset retrievedDateTimeOffset = randomDateTimeOffset;

            List<ListenerEventV2> expectedListenerEventV2s =
                retrievedEventV2s.SelectMany(eventV2 =>
                    retrievedEventListenerV2s.Select(eventListenerV2 =>
                        new ListenerEventV2
                        {
                            EventListenerId = eventListenerV2.Id,
                            EventId = eventV2.Id,
                            Status = ListenerEventV2Status.Pending,
                            EventAddressId = eventV2.EventAddressId,
                            CreatedDate = eventV2.CreatedDate,
                            UpdatedDate = eventV2.UpdatedDate
                        })).ToList();


            List<ListenerEventV2> expectedListenerEventV2sOnModify =
                expectedListenerEventV2s.DeepClone();

            List<EventCallV2> expectedCallEventV2s =
                retrievedEventV2s.SelectMany(eventV2 =>
                    retrievedEventListenerV2s.Select(
                        retrievedEventListenerV2 =>
                            new EventCallV2
                            {
                                Endpoint = retrievedEventListenerV2.Endpoint,
                                Content = eventV2.Content,
                                Secret = retrievedEventListenerV2.HeaderSecret,
                            })).ToList();

            List<Exception> eventCallExceptions =
                expectedCallEventV2s.Select(eventCall =>
                    new Exception(message: GetRandomString()))
                        .ToList();

            var ranEventCallV2s = new List<EventCallV2>();

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV2sAsync())
                    .ReturnsAsync(retrievedEventV2s);

            foreach (EventV2 eventV2 in retrievedEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId))
                            .ReturnsAsync(retrievedEventListenerV2s);
            }

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(retrievedDateTimeOffset);

            foreach (ListenerEventV2 expectedListenerEventV2 in expectedListenerEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.AddListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))))
                            .ReturnsAsync(expectedListenerEventV2.DeepClone());
            }

            for (int i = 0; i < expectedCallEventV2s.Count; i++)
            {
                var ranEventCall = new EventCallV2
                {
                    Endpoint = expectedCallEventV2s[i].Endpoint,
                    Content = expectedCallEventV2s[i].Content,
                    Response = eventCallExceptions[i].Message
                };

                this.eventV2OrchestrationServiceMock.Setup(service =>
                    service.RunEventCallV2Async(
                        It.Is(SameEventCallAs(expectedCallEventV2s[i]))))
                            .ThrowsAsync(eventCallExceptions[i]);

                ranEventCallV2s.Add(ranEventCall);
            }

            for (int i = 0; i < expectedListenerEventV2s.Count(); i++)
            {
                expectedListenerEventV2sOnModify[i].UpdatedDate = retrievedDateTimeOffset;
                expectedListenerEventV2sOnModify[i].Status = ListenerEventV2Status.Error;
                expectedListenerEventV2sOnModify[i].Response = ranEventCallV2s[i].Response;

                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.ModifyListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2sOnModify[i]))))
                            .ReturnsAsync(expectedListenerEventV2sOnModify[i]);
            }

            // when
            await this.eventV2CoordinationService
                .FireScheduledPendingEventV2sAsync();

            // then
            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV2sAsync(),
                    Times.Once);

            foreach (EventV2 eventV2 in retrievedEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId),
                            Times.Once);
            }

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Exactly(callCount: expectedListenerEventV2s.Count()));

            foreach (ListenerEventV2 expectedListenerEventV2 in expectedListenerEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.AddListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))),
                            Times.Once);
            }

            foreach (EventCallV2 expectedCallEventV2 in expectedCallEventV2s)
            {
                this.eventV2OrchestrationServiceMock.Verify(service =>
                    service.RunEventCallV2Async(
                        It.Is(SameEventCallAs(expectedCallEventV2))),
                            Times.Once);
            }

            foreach (ListenerEventV2 expectedListenerEventV2 in expectedListenerEventV2sOnModify)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.ModifyListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))),
                            Times.Once);
            }

            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
