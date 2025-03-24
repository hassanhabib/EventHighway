﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private void ConfigureListenerEventV2s(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListenerEventV1>()
                .HasOne(listenerEventV2 => listenerEventV2.Event)
                .WithMany(eventV2 => eventV2.ListenerEvents)
                .HasForeignKey(listenerEventV2 => listenerEventV2.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ListenerEventV1>()
                .HasOne(listenerEventV2 => listenerEventV2.EventAddress)
                .WithMany(eventAddressV2 => eventAddressV2.ListenerEvents)
                .HasForeignKey(listenerEventV2 => listenerEventV2.EventAddressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ListenerEventV1>()
                .HasOne(listenerEventV2 => listenerEventV2.EventListener)
                .WithMany(eventListenerV2 => eventListenerV2.ListenerEvents)
                .HasForeignKey(listenerEventV2 => listenerEventV2.EventListenerId)
                .OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
