﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<ListenerEvent> InsertListenerEventAsync(ListenerEvent listenerEvent);
        ValueTask<ListenerEvent> UpdateListenerEventAsync(ListenerEvent listenerEvent);
    }
}
