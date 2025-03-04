﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Services.Foundations.Events.V2;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V2
{
    public partial class EventV2ServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventV2Service eventV2Service;

        public EventV2ServiceTests()
        {
            this.storageBrokerMock =
                new Mock<IStorageBroker>();

            this.loggingBrokerMock =
                new Mock<ILoggingBroker>();

            this.eventV2Service = new EventV2Service(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static EventV2 CreateRandomEventV2() =>
            CreateEventV2Filler().Create();

        private static DateTimeOffset CreateRandomDateTime()
        {
            return new DateTimeRange(
                earliestDate: DateTime.UnixEpoch)
                    .GetValue();
        }

        private static Filler<EventV2> CreateEventV2Filler()
        {
            var filler = new Filler<EventV2>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(CreateRandomDateTime)

                .OnType<DateTimeOffset?>()
                    .Use(CreateRandomDateTime());

            return filler;
        }
    }
}
