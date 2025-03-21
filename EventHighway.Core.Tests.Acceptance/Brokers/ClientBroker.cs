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
        string connectionString = String.Concat(
            "Server=(localdb)\\MSSQLLocalDB;Database=EventHighwayDb;",
            "Trusted_Connection=True;MultipleActiveResultSets=true;" +
            "Min Pool Size=5;Max Pool Size=1000;");

        private readonly SqlConnection sqlConnection;
        private readonly IEventHighwayClient eventHighwayClient;

        public ClientBroker()
        {
            this.eventHighwayClient =
                new EventHighwayClient(connectionString);
        }

        public async ValueTask DisposeAsync()
        {
            using (var sqlConnection =
                new SqlConnection(connectionString))
            {
                await sqlConnection.CloseAsync();
                await sqlConnection.DisposeAsync();
            }
        }
    }
}
