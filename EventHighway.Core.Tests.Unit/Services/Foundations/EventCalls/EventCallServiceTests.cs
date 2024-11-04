// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Brokers.Apis;
using EventHighway.Core.Models.EventCall;
using EventHighway.Core.Services.Foundations.EventCalls;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.EventCalls
{
    public partial class EventCallServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly IEventCallService eventCallService;

        public EventCallServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();

            this.eventCallService = new EventCallService(
                apiBroker: this.apiBrokerMock.Object);
        }

        private static EventCall CreateRandomEventCall() =>
            CreateEventCallFiller().Create();

        private static string CreateRandomResponse() =>
            new MnemonicString().GetValue();

        private static Filler<EventCall> CreateEventCallFiller() =>
            new Filler<EventCall>();
    }
}
