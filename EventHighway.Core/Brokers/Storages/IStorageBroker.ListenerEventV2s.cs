// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents.V2;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<ListenerEventV2> InsertListenerEventV2Async(ListenerEventV2 listenerEventV2);
        ValueTask<ListenerEventV2> SelectListenerEventV2ByIdAsync(Guid listenerEventV2Id);
        ValueTask<ListenerEventV2> UpdateListenerEventV2Async(ListenerEventV2 listenerEventV2);
    }
}
