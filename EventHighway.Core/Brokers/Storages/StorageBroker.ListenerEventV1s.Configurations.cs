// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private void ConfigureListenerEventV1s(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListenerEventV1>()
                .HasOne(listenerEventV1 => listenerEventV1.Event)
                .WithMany(eventV1 => eventV1.ListenerEvents)
                .HasForeignKey(listenerEventV1 => listenerEventV1.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ListenerEventV1>()
                .HasOne(listenerEventV1 => listenerEventV1.EventAddress)
                .WithMany(eventAddressV1 => eventAddressV1.ListenerEvents)
                .HasForeignKey(listenerEventV1 => listenerEventV1.EventAddressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ListenerEventV1>()
                .HasOne(listenerEventV1 => listenerEventV1.EventListener)
                .WithMany(eventListenerV1 => eventListenerV1.ListenerEvents)
                .HasForeignKey(listenerEventV1 => listenerEventV1.EventListenerId)
                .OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
