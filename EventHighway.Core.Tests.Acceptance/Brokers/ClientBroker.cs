// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Clients.EventHighways;
using Microsoft.Data.SqlClient;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker : IAsyncDisposable
    {
        private readonly SqlConnection sqlConnection;
        private readonly IEventHighwayClient eventHighwayClient;

        public ClientBroker()
        {
            string connectionString = String.Concat(
                "Server=(localdb)\\MSSQLLocalDB;Database=EventHighwayDb99;",
                "Trusted_Connection=True;MultipleActiveResultSets=true;Max Pool Size=50;");

            this.sqlConnection =
                new SqlConnection(connectionString);

            this.eventHighwayClient =
                new EventHighwayClient(connectionString);
        }

        public async ValueTask DisposeAsync() =>
            await this.sqlConnection.DisposeAsync();
    }
}
