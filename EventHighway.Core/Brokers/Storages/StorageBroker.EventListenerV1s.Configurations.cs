// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private void ConfigureEventListenerV1s(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventListenerV1>()
                .HasOne(eventListenerV1 => eventListenerV1.EventAddress)
                .WithMany(eventAddressV1 => eventAddressV1.EventListeners)
                .HasForeignKey(eventListenerV1 => eventListenerV1.EventAddressId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
