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
        ValueTask<ListenerEventV1> InsertListenerEventV1Async(ListenerEventV1 listenerEventV1);
        ValueTask<IQueryable<ListenerEventV1>> SelectAllListenerEventV1sAsync();
        ValueTask<ListenerEventV1> SelectListenerEventV1ByIdAsync(Guid listenerEventV1Id);
        ValueTask<ListenerEventV1> UpdateListenerEventV1Async(ListenerEventV1 listenerEventV1);
        ValueTask<ListenerEventV1> DeleteListenerEventV1Async(ListenerEventV1 listenerEventV1);
    }
}
