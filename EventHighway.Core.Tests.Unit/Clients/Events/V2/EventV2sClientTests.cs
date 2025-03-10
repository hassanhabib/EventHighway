// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Clients.Events.V2;
using EventHighway.Core.Services.Coordinations.Events.V2;
using Moq;

namespace EventHighway.Core.Tests.Unit.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        private readonly Mock<IEventV2CoordinationService> eventV2CoordinationServiceMock;
        private readonly IEventV2sClient eventV2SClient;

        public EventV2sClientTests()
        {
            this.eventV2CoordinationServiceMock =
                new Mock<IEventV2CoordinationService>();

            this.eventV2SClient =
                new EventV2sClient(
                    eventV2CoordinationService:
                        this.eventV2CoordinationServiceMock.Object);
        }
    }
}
