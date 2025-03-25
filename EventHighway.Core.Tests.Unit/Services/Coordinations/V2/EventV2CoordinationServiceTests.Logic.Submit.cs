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
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        [Fact]
        public async Task ShouldSubmitImmediateEventV2WhenScheduledDateIsNullOrInPastAsync()
        {
            // given
            int randomDays = GetRandomNumber();
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            DateTimeOffset retrievedDateTimeOffset = randomDateTimeOffset;
            EventV1 randomEventV2 = CreateRandomEventV2();
            EventV1 inputEventV2 = randomEventV2;

            inputEventV2.ScheduledDate =
                retrievedDateTimeOffset.AddDays(randomDays);

            EventV1 inputScheduledEventV2 = inputEventV2;
            inputScheduledEventV2.Type = EventV1Type.Scheduled;
            EventV1 submittedEventV2 = inputScheduledEventV2;
            EventV1 expectedEventV2 = submittedEventV2.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(retrievedDateTimeOffset);

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.SubmitEventV1Async(inputScheduledEventV2))
                    .ReturnsAsync(submittedEventV2);

            // when
            EventV1 actualEventV2 =
                await this.eventV2CoordinationService
                    .SubmitEventV2Async(inputEventV2);

            // then
            actualEventV2.Should().BeEquivalentTo(expectedEventV2);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.SubmitEventV1Async(inputScheduledEventV2),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV1sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV1Async(
                    It.IsAny<ListenerEventV1>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV1Async(
                    It.IsAny<EventCallV1>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV1Async(
                    It.IsAny<ListenerEventV1>()),
                        Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ScheduledDates))]
        public async Task ShouldSubmitScheduledEventV2WhenScheduledDateIsInFutureAsync(
            DateTimeOffset randomDateTimeOffset,
            DateTimeOffset? scheduledDate)
        {
            // given
            var mockSequence = new MockSequence();
            EventV1 randomEventV2 = CreateRandomEventV2();
            EventV1 inputEventV2 = randomEventV2;
            inputEventV2.ScheduledDate = scheduledDate;
            EventV1 inputImmediateEventV2 = inputEventV2;
            inputImmediateEventV2.Type = EventV1Type.Immediate;
            EventV1 submittedEventV2 = inputImmediateEventV2;
            EventV1 expectedEventV2 = submittedEventV2.DeepClone();

            IQueryable<EventListenerV1> randomEventListenerV2s =
                CreateRandomEventListenerV2s();

            IQueryable<EventListenerV1> retrievedEventListenerV2s =
                randomEventListenerV2s;

            List<ListenerEventV1> inputListenerEventV2s =
                retrievedEventListenerV2s.Select(eventListenerV2 =>
                    new ListenerEventV1
                    {
                        EventListenerId = eventListenerV2.Id,
                        EventId = inputImmediateEventV2.Id,
                        Status = ListenerEventV1Status.Pending,
                        EventAddressId = inputImmediateEventV2.EventAddressId,
                        CreatedDate = inputImmediateEventV2.CreatedDate,
                        UpdatedDate = inputImmediateEventV2.UpdatedDate
                    }).ToList();

            List<ListenerEventV1> addedListenerEventV2s =
                inputListenerEventV2s.DeepClone();

            List<ListenerEventV1> modifiedListenerEventV2s =
                addedListenerEventV2s;

            List<ListenerEventV1> expectedListenerEventV2s =
                modifiedListenerEventV2s.DeepClone();

            List<EventCallV1> expectedInputCallEventV2s =
                retrievedEventListenerV2s.Select(
                    retrievedEventListenerV2 =>
                        new EventCallV1
                        {
                            Endpoint = retrievedEventListenerV2.Endpoint,
                            Content = inputImmediateEventV2.Content,
                            Secret = retrievedEventListenerV2.HeaderSecret,
                        }).ToList();

            var ranEventCallV2s = new List<EventCallV1>();

            this.dateTimeBrokerMock.InSequence(mockSequence).Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ReturnsAsync(randomDateTimeOffset);

            this.eventV2OrchestrationServiceMock
                .InSequence(mockSequence).Setup(service =>
                    service.SubmitEventV1Async(inputImmediateEventV2))
                        .ReturnsAsync(submittedEventV2);

            this.eventListenerV2OrchestrationServiceMock
                .InSequence(mockSequence).Setup(service =>
                    service.RetrieveEventListenerV1sByEventAddressIdAsync(
                        inputImmediateEventV2.EventAddressId))
                            .ReturnsAsync(retrievedEventListenerV2s);

            for (int index = 0; index < inputListenerEventV2s.Count; index++)
            {
                this.eventListenerV2OrchestrationServiceMock
                    .InSequence(mockSequence).Setup(service =>
                        service.AddListenerEventV1Async(
                            It.Is(SameListenerEventAs(inputListenerEventV2s[index]))))
                                .ReturnsAsync(addedListenerEventV2s[index]);

                var ranEventCall = new EventCallV1
                {
                    Endpoint = expectedInputCallEventV2s[index].Endpoint,
                    Content = expectedInputCallEventV2s[index].Content,
                    Response = GetRandomString()
                };

                this.eventV2OrchestrationServiceMock
                    .InSequence(mockSequence).Setup(service =>
                        service.RunEventCallV1Async(
                            It.Is(SameEventCallAs(expectedInputCallEventV2s[index]))))
                                .ReturnsAsync(ranEventCall);

                ranEventCallV2s.Add(item: ranEventCall);

                this.dateTimeBrokerMock.InSequence(mockSequence).Setup(broker =>
                    broker.GetDateTimeOffsetAsync())
                        .ReturnsAsync(randomDateTimeOffset);

                addedListenerEventV2s[index].UpdatedDate = randomDateTimeOffset;
                addedListenerEventV2s[index].Status = ListenerEventV1Status.Success;
                addedListenerEventV2s[index].Response = ranEventCallV2s[index].Response;

                this.eventListenerV2OrchestrationServiceMock
                    .InSequence(mockSequence).Setup(service =>
                        service.ModifyListenerEventV1Async(
                            It.Is(SameListenerEventAs(addedListenerEventV2s[index]))))
                                .ReturnsAsync(modifiedListenerEventV2s[index]);
            }

            // when
            EventV1 actualEventV2 =
                await this.eventV2CoordinationService
                    .SubmitEventV2Async(inputEventV2);

            // then
            actualEventV2.Should().BeEquivalentTo(expectedEventV2);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Exactly(callCount: inputListenerEventV2s.Count + 1));

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.SubmitEventV1Async(inputImmediateEventV2),
                    Times.Once);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV1sByEventAddressIdAsync(
                    inputImmediateEventV2.EventAddressId),
                        Times.Once);

            for (int index = 0; index < inputListenerEventV2s.Count; index++)
            {
                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.AddListenerEventV1Async(
                        It.Is(SameListenerEventAs(inputListenerEventV2s[index]))),
                            Times.Once);

                this.eventV2OrchestrationServiceMock.Verify(service =>
                    service.RunEventCallV1Async(
                        It.Is(SameEventCallAs(expectedInputCallEventV2s[index]))),
                            Times.Once);

                this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                    service.ModifyListenerEventV1Async(
                        It.Is(SameListenerEventAs(addedListenerEventV2s[index]))),
                            Times.Once);
            }

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
