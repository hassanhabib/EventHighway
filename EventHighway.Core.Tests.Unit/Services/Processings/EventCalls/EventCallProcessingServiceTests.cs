// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.EventCall;
using EventHighway.Core.Services.Foundations.EventCalls;
using EventHighway.Core.Services.Processings.EventCalls;
using Moq;
using Tynamix.ObjectFiller;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventCalls
{
    public partial class EventCallProcessingServiceTests
    {
        private readonly Mock<IEventCallService> eventCallServiceMock;
        private readonly IEventCallProcessingService eventCallProcessingService;

        public EventCallProcessingServiceTests()
        {
            this.eventCallServiceMock = new Mock<IEventCallService>();

            this.eventCallProcessingService = new EventCallProcessingService(
                eventCallService: this.eventCallServiceMock.Object);
        }

        private static EventCall CreateRandomEventCall() =>
            CreateEventCallFiller().Create();

        private static string CreateRandomResponse() =>
            new MnemonicString().GetValue();

        private static Filler<EventCall> CreateEventCallFiller() =>
            new Filler<EventCall>();
    }
}
