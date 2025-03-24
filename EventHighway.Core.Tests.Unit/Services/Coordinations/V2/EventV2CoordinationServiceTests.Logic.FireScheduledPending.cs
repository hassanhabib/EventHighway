// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
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
            IQueryable<EventV1> randomEventV2s = CreateRandomEventV2s();
            IQueryable<EventV1> retrievedEventV2s = randomEventV2s;

            IQueryable<EventListenerV1> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            IQueryable<EventListenerV1> retrievedEventListenerV2s =
                randomEventListenerV2s;

            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            DateTimeOffset retrievedDateTimeOffset = randomDateTimeOffset;

            List<ListenerEventV1> inputListenerEventV2s =
                retrievedEventV2s.SelectMany(eventV2 =>
                    retrievedEventListenerV2s.Select(eventListenerV2 =>
                        new ListenerEventV1
                        {
                            EventListenerId = eventListenerV2.Id,
                            EventId = eventV2.Id,
                            Status = ListenerEventV1Status.Pending,
                            EventAddressId = eventV2.EventAddressId,
                            CreatedDate = eventV2.CreatedDate,
                            UpdatedDate = eventV2.UpdatedDate
                        })).ToList();

            List<ListenerEventV1> expectedListenerEventV2s =
                inputListenerEventV2s.DeepClone();

            List<EventCallV1> expectedCallEventV2s =
                retrievedEventV2s.SelectMany(eventV2 =>
                    retrievedEventListenerV2s.Select(
                        retrievedEventListenerV2 =>
                            new EventCallV1
                            {
                                Endpoint = retrievedEventListenerV2.Endpoint,
                                Content = eventV2.Content,
                                Secret = retrievedEventListenerV2.HeaderSecret,
                            })).ToList();

            var ranEventCallV2s = new List<EventCallV1>();

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV1sAsync())
                    .ReturnsAsync(retrievedEventV2s);

            foreach (EventV1 eventV2 in retrievedEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId))
                            .ReturnsAsync(retrievedEventListenerV2s);
            }

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(retrievedDateTimeOffset);

            foreach (ListenerEventV1 expectedListenerEventV2 in inputListenerEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.AddListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))))
                            .ReturnsAsync(expectedListenerEventV2.DeepClone());
            }

            foreach (EventCallV1 expectedCallEventV2 in expectedCallEventV2s)
            {
                var ranEventCall = new EventCallV1
                {
                    Endpoint = expectedCallEventV2.Endpoint,
                    Content = expectedCallEventV2.Content,
                    Response = GetRandomString()
                };

                this.eventV2OrchestrationServiceMock.Setup(service =>
                    service.RunEventCallV1Async(
                        It.Is(SameEventCallAs(expectedCallEventV2))))
                            .ReturnsAsync(ranEventCall);

                ranEventCallV2s.Add(item: ranEventCall);
            }

            for (int index = 0; index < inputListenerEventV2s.Count; index++)
            {
                expectedListenerEventV2s[index].UpdatedDate = retrievedDateTimeOffset;
                expectedListenerEventV2s[index].Status = ListenerEventV1Status.Success;
                expectedListenerEventV2s[index].Response = ranEventCallV2s[index].Response;

                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.ModifyListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2s[index]))))
                            .ReturnsAsync(expectedListenerEventV2s[index]);
            }

            // when
            await this.eventV2CoordinationService
                .FireScheduledPendingEventV2sAsync();

            // then
            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV1sAsync(),
                    Times.Once);

            foreach (EventV1 eventV2 in retrievedEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId),
                            Times.Once);
            }

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Exactly(callCount: inputListenerEventV2s.Count));

            foreach (ListenerEventV1 expectedListenerEventV2 in inputListenerEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.AddListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))),
                            Times.Once);
            }

            foreach (EventCallV1 expectedCallEventV2 in expectedCallEventV2s)
            {
                this.eventV2OrchestrationServiceMock.Verify(service =>
                    service.RunEventCallV1Async(
                        It.Is(SameEventCallAs(expectedCallEventV2))),
                            Times.Once);
            }

            foreach (ListenerEventV1 expectedListenerEventV2 in expectedListenerEventV2s)
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
            IQueryable<EventV1> randomEventV2s = CreateRandomEventV2s();
            IQueryable<EventV1> retrievedEventV2s = randomEventV2s;

            IQueryable<EventListenerV1> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            IQueryable<EventListenerV1> retrievedEventListenerV2s =
                randomEventListenerV2s;

            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            DateTimeOffset retrievedDateTimeOffset = randomDateTimeOffset;

            List<ListenerEventV1> expectedListenerEventV2s =
                retrievedEventV2s.SelectMany(eventV2 =>
                    retrievedEventListenerV2s.Select(eventListenerV2 =>
                        new ListenerEventV1
                        {
                            EventListenerId = eventListenerV2.Id,
                            EventId = eventV2.Id,
                            Status = ListenerEventV1Status.Pending,
                            EventAddressId = eventV2.EventAddressId,
                            CreatedDate = eventV2.CreatedDate,
                            UpdatedDate = eventV2.UpdatedDate
                        })).ToList();


            List<ListenerEventV1> expectedListenerEventV2sOnModify =
                expectedListenerEventV2s.DeepClone();

            List<EventCallV1> expectedCallEventV2s =
                retrievedEventV2s.SelectMany(eventV2 =>
                    retrievedEventListenerV2s.Select(
                        retrievedEventListenerV2 =>
                            new EventCallV1
                            {
                                Endpoint = retrievedEventListenerV2.Endpoint,
                                Content = eventV2.Content,
                                Secret = retrievedEventListenerV2.HeaderSecret,
                            })).ToList();

            List<Exception> eventCallExceptions =
                expectedCallEventV2s.Select(eventCall =>
                    new Exception(message: GetRandomString()))
                        .ToList();

            var ranEventCallV2s = new List<EventCallV1>();

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RetrieveScheduledPendingEventV1sAsync())
                    .ReturnsAsync(retrievedEventV2s);

            foreach (EventV1 eventV2 in retrievedEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId))
                            .ReturnsAsync(retrievedEventListenerV2s);
            }

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(retrievedDateTimeOffset);

            foreach (ListenerEventV1 expectedListenerEventV2 in expectedListenerEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.AddListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))))
                            .ReturnsAsync(expectedListenerEventV2.DeepClone());
            }

            for (int index = 0; index < expectedCallEventV2s.Count; index++)
            {
                var ranEventCall = new EventCallV1
                {
                    Endpoint = expectedCallEventV2s[index].Endpoint,
                    Content = expectedCallEventV2s[index].Content,
                    Response = eventCallExceptions[index].Message
                };

                this.eventV2OrchestrationServiceMock.Setup(service =>
                    service.RunEventCallV1Async(
                        It.Is(SameEventCallAs(expectedCallEventV2s[index]))))
                            .ThrowsAsync(eventCallExceptions[index]);

                ranEventCallV2s.Add(item: ranEventCall);
            }

            for (int index = 0; index < expectedListenerEventV2s.Count; index++)
            {
                expectedListenerEventV2sOnModify[index].UpdatedDate = retrievedDateTimeOffset;
                expectedListenerEventV2sOnModify[index].Status = ListenerEventV1Status.Error;
                expectedListenerEventV2sOnModify[index].Response = ranEventCallV2s[index].Response;

                this.eventListenerV2OrchestrationServiceMock.Setup(service =>
                    service.ModifyListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2sOnModify[index]))))
                            .ReturnsAsync(expectedListenerEventV2sOnModify[index]);
            }

            // when
            await this.eventV2CoordinationService
                .FireScheduledPendingEventV2sAsync();

            // then
            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveScheduledPendingEventV1sAsync(),
                    Times.Once);

            foreach (EventV1 eventV2 in retrievedEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.RetrieveEventListenerV2sByEventAddressIdAsync(
                        eventV2.EventAddressId),
                            Times.Once);
            }

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Exactly(callCount: expectedListenerEventV2s.Count));

            foreach (ListenerEventV1 expectedListenerEventV2 in expectedListenerEventV2s)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.AddListenerEventV2Async(
                        It.Is(SameListenerEventAs(expectedListenerEventV2))),
                            Times.Once);
            }

            foreach (EventCallV1 expectedCallEventV2 in expectedCallEventV2s)
            {
                this.eventV2OrchestrationServiceMock.Verify(service =>
                    service.RunEventCallV1Async(
                        It.Is(SameEventCallAs(expectedCallEventV2))),
                            Times.Once);
            }

            foreach (ListenerEventV1 expectedListenerEventV2 in expectedListenerEventV2sOnModify)
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
