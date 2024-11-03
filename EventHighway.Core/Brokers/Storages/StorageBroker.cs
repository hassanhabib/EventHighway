// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EFxceptions;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly string connectionString;

        public StorageBroker(string connectionString) =>
            this.connectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
             optionsBuilder.UseSqlServer(this.connectionString);
    }
}
