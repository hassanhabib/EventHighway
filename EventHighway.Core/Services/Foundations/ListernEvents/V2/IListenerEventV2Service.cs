// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;

namespace EventHighway.Core.Services.Foundations.ListernEvents.V2
{
    internal interface IListenerEventV2Service
    {
        ValueTask<ListenerEventV2> AddListenerEventV2Async(ListenerEventV2 listenerEventV2);
        ValueTask<IQueryable<ListenerEventV2>> RetrieveAllListenerEventV2sAsync();
        ValueTask<ListenerEventV2> ModifyListenerEventV2Async(ListenerEventV2 listenerEventV2);
        ValueTask<ListenerEventV2> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id);
    }
}
