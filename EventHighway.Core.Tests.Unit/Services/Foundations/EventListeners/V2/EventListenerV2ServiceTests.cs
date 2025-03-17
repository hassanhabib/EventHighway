// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Services.Foundations.EventListeners.V2;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly IEventListenerV2Service eventListenerV2Service;

        public EventListenerV2ServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            this.eventListenerV2Service = new EventListenerV2Service(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static SqlException GetSqlException()
        {
            return (SqlException)RuntimeHelpers
                .GetUninitializedObject(type: typeof(SqlException));
        }

        private static Guid GetRandomId() =>
            Guid.NewGuid();

        private static IQueryable<EventListenerV2> CreateRandomEventListenerV2s()
        {
            return CreateEventListenerV2Filler(
                dates: GetRandomDateTime())
                    .Create(count: GetRandomNumber())
                        .AsQueryable();
        }

        private static EventListenerV2 CreateRandomEventListenerV2() =>
            CreateEventListenerV2Filler(dates: GetRandomDateTime()).Create();

        private static EventListenerV2 CreateRandomEventListenerV2(DateTimeOffset dates) =>
            CreateEventListenerV2Filler(dates).Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Filler<EventListenerV2> CreateEventListenerV2Filler(DateTimeOffset dates)
        {
            var filler = new Filler<EventListenerV2>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.EventAddress).IgnoreIt()

                .OnProperty(eventListenerV2 =>
                    eventListenerV2.ListenerEvents).IgnoreIt();

            return filler;
        }
    }
}
