﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Services.Foundations.Events.V1;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private static void ConfigureEventV1s(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventV1>()
                .HasOne(eventV1 => eventV1.EventAddress)
                .WithMany(eventAddressV1 => eventAddressV1.Events)
                .HasForeignKey(eventV1 => eventV1.EventAddressId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
