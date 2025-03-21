// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Clients.EventHighways;
using Microsoft.Data.SqlClient;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker : IDisposable
    {
        private readonly SqlConnection sqlConnection;
        private readonly IEventHighwayClient eventHighwayClient;

        public ClientBroker()
        {
            string connectionString = String.Concat(
                "Server=(localdb)\\MSSQLLocalDB;Database=EventHighwayDb10;",
                "Trusted_Connection=True;MultipleActiveResultSets=true;Max Pool Size=200;");

            this.sqlConnection = 
                new SqlConnection(connectionString);

            this.eventHighwayClient =
                new EventHighwayClient(connectionString);
        }

        public void Dispose() =>
            this.sqlConnection.Dispose();
    }
}
