// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.EventCall;
using EventHighway.Core.Models.EventListeners;
using EventHighway.Core.Models.Events;
using EventHighway.Core.Models.ListenerEvents;
using EventHighway.Core.Services.Orchestrations.EventListeners;
using EventHighway.Core.Services.Processings.EventCalls;
using EventHighway.Core.Services.Processings.EventListeners;
using EventHighway.Core.Services.Processings.ListenerEvents;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.Orchestrations.EventListeners
{
    public partial class EventListenerOrchestrationServiceTests
    {
        private readonly Mock<IEventListenerProcessingService> eventListenerProcessingServiceMock;
        private readonly Mock<IListenerEventProcessingService> listenerEventProcessingServiceMock;
        private readonly Mock<IEventCallProcessingService> eventCallProcessingServiceMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IEventListenerOrchestrationService eventListenerOrchestrationService;

        public EventListenerOrchestrationServiceTests()
        {
            this.eventListenerProcessingServiceMock =
                new Mock<IEventListenerProcessingService>();
            
            this.listenerEventProcessingServiceMock =
                new Mock<IListenerEventProcessingService>();
            
            this.eventCallProcessingServiceMock =
                new Mock<IEventCallProcessingService>();
            
            this.dateTimeBrokerMock =
                new Mock<IDateTimeBroker>();

            this.compareLogic = new CompareLogic();

            this.eventListenerOrchestrationService = new EventListenerOrchestrationService(
                eventListenerProcessingService: this.eventListenerProcessingServiceMock.Object,
                listenerEventProcessingService: this.listenerEventProcessingServiceMock.Object,
                eventCallProcessingService: this.eventCallProcessingServiceMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        private static Event CreateRandomEvent() =>
            CreateEventFiller().Create();

        private static IQueryable<EventListener> CreateRandomEventListeners() =>
            CreateEventListenerFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string CreateRandomEventCallResponse() =>
            new MnemonicString().GetValue();

        private Expression<Func<ListenerEvent, bool>> SameListenerEventAs(
           ListenerEvent expectedListenerEvent)
        {
            return actualListenerEvent =>
                this.compareLogic.Compare(expectedListenerEvent, actualListenerEvent)
                    .AreEqual;
        }

        private Expression<Func<EventCall, bool>> SameEventCallAs(
           EventCall expectedEventCall)
        {
            return actualEventCall =>
                this.compareLogic.Compare(expectedEventCall, actualEventCall)
                    .AreEqual;
        }

        private static Filler<Event> CreateEventFiller()
        {
            var filler = new Filler<Event>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime);

            return filler;
        }

        private static Filler<EventListener> CreateEventListenerFiller()
        {
            var filler = new Filler<EventListener>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime);

            return filler;
        }
    }
}
