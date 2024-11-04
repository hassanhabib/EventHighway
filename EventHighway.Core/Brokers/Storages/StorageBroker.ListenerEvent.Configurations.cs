// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.ListenerEvents;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private void ConfigureListenerEvent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListenerEvent>()
                .HasOne(listenerEvent => listenerEvent.Event)
                .WithMany(@event => @event.ListenerEvents)
                .HasForeignKey(listenerEvent => listenerEvent.EventId);

            modelBuilder.Entity<ListenerEvent>()
                .HasOne(listenerEvent => listenerEvent.EventAddress)
                .WithMany(eventAddress => eventAddress.ListenerEvents)
                .HasForeignKey(listenerEvent => listenerEvent.EventAddressId);

            modelBuilder.Entity<ListenerEvent>()
                .HasOne(listenerEvent => listenerEvent.EventListener)
                .WithMany(eventListener => eventListener.ListenerEvents)
                .HasForeignKey(listenerEvent => listenerEvent.EventListenerId);
        }
    }
}
