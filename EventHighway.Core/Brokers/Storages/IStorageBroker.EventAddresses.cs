﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<EventAddress> InsertEventAddressAsync(EventAddress eventAddress);
        ValueTask<IQueryable<EventAddress>> SelectAllEventAddressesAsync();
        ValueTask<EventAddress> SelectEventAddressByIdAsync(Guid eventAddressId);
    }
}
