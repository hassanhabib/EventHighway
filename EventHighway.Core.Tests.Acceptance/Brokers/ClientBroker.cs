// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Clients.EventHighways;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker
    {
        private readonly IEventHighwayClient eventHighwayClient;

        public ClientBroker(string connectionString)
        {
            this.eventHighwayClient =
                new EventHighwayClient(connectionString);
        }
    }
}
