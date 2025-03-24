// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal interface IEventListenerV2OrchestrationService
    {
        ValueTask<EventListenerV1> AddEventListenerV2Async(EventListenerV1 eventListenerV2);
        ValueTask<IQueryable<EventListenerV1>> RetrieveEventListenerV2sByEventAddressIdAsync(Guid eventAddressId);
        ValueTask<EventListenerV1> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id);
        ValueTask<ListenerEventV2> AddListenerEventV2Async(ListenerEventV2 listenerEventV2);
        ValueTask<IQueryable<ListenerEventV2>> RetrieveAllListenerEventV2sAsync();
        ValueTask<ListenerEventV2> ModifyListenerEventV2Async(ListenerEventV2 listenerEventV2);
        ValueTask<ListenerEventV2> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id);
    }
}
