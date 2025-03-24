// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<ListenerEventV1> InsertListenerEventV2Async(ListenerEventV1 listenerEventV2);
        ValueTask<IQueryable<ListenerEventV1>> SelectAllListenerEventV2sAsync();
        ValueTask<ListenerEventV1> SelectListenerEventV2ByIdAsync(Guid listenerEventV2Id);
        ValueTask<ListenerEventV1> UpdateListenerEventV2Async(ListenerEventV1 listenerEventV2);
        ValueTask<ListenerEventV1> DeleteListenerEventV2Async(ListenerEventV1 listenerEventV2);
    }
}
