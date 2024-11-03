using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;
using Microsoft.EntityFrameworkCore;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private void ConfigureEventListeners(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventListener>()
                .HasOne(eventListener => eventListener.EventAddress)
                .WithMany(eventAddress => eventAddress.EventListeners)
                .HasForeignKey(eventListener => eventListener.EventAddressId);
        }
    }
}
