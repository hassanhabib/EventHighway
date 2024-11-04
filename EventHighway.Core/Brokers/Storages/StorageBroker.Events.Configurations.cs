// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Events;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private static void ConfigureEvents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasOne(@event => @event.EventAddress)
                .WithMany(eventAddress => eventAddress.Events)
                .HasForeignKey(@event => @event.EventAddressId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
