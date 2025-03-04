// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Events.V2;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private static void ConfigureEventV2s(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventV2>()
                .HasOne(eventV2 => eventV2.EventAddress)
                .WithMany(eventAddressV2 => eventAddressV2.Events)
                .HasForeignKey(eventV2 => eventV2.EventAddressId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
