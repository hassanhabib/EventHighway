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
        private readonly string connectionString = String.Concat(
            "Server=(localdb)\\MSSQLLocalDB;Database=EventHighwayDb;",
            "Trusted_Connection=True;MultipleActiveResultSets=true;");

        private readonly SqlConnection sqlConnection;
        private readonly IEventHighwayClient eventHighwayClient;

        public ClientBroker()
        {
            this.eventHighwayClient =
                new EventHighwayClient(this.connectionString);
        }

        public async ValueTask DisposeAsync()
        {
            using (var sqlConnection =
                new SqlConnection(this.connectionString))
            {
                await sqlConnection.CloseAsync();
                await sqlConnection.DisposeAsync();
            }
        }
    }
}
