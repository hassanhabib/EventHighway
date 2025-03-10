﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using EventHighway.Core.Brokers.Apis;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Services.Foundations.EventCalls.V2;
using Moq;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventCalls.V2
{
    public partial class EventCallV2ServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEventCallV2Service eventCallV2Service;

        public EventCallV2ServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.eventCallV2Service = new EventCallV2Service(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<Xeption> CriticalDependencyExceptions()
        {
            return new TheoryData<Xeption>
            {
                new HttpResponseUrlNotFoundException(),
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException(),
                new HttpResponseMethodNotAllowedException()
            };
        }

        private static HttpResponseBadRequestException CreateHttpBadRequestException()
        {
            string someExceptionMessage = GetRandomString();
            var httpResponseMessage = new HttpResponseMessage();
            Dictionary<string, List<string>> randomDictionary = CreateRandomDictionary();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(
                    httpResponseMessage,
                    someExceptionMessage);

            httpResponseBadRequestException.AddData(randomDictionary);

            return httpResponseBadRequestException;
        }

        private static Dictionary<string, List<string>> CreateRandomDictionary()
        {
            var randomDictionary = new Dictionary<string, List<string>>();
            int randomCount = GetRandomNumber();

            Enumerable.Range(start: 0, count: randomCount)
                .ToList().ForEach(item => randomDictionary.TryAdd(
                    key: GetRandomString(),
                    value: GetRandomStrings()));

            return randomDictionary;
        }

        private static List<string> GetRandomStrings()
        {
            var randomList = new List<string>();
            int randomCount = GetRandomNumber();

            Enumerable.Range(start: 0, count: randomCount)
                .ToList().ForEach(count => randomList.Add(
                    item: GetRandomString()));

            return randomList;
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static EventCallV2 CreateRandomEventCallV2() =>
            CreateEventCallV2Filler().Create();

        private static string CreateRandomResponse() =>
            new MnemonicString().GetValue();

        private static Filler<EventCallV2> CreateEventCallV2Filler() =>
            new Filler<EventCallV2>();
    }
}
