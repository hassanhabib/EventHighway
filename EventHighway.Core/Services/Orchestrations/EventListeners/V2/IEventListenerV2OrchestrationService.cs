// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal interface IEventListenerV2OrchestrationService
    {
        ValueTask<EventListenerV1> AddEventListenerV2Async(EventListenerV1 eventListenerV2);
        ValueTask<IQueryable<EventListenerV1>> RetrieveEventListenerV2sByEventAddressIdAsync(Guid eventAddressId);
        ValueTask<EventListenerV1> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id);
        ValueTask<ListenerEventV1> AddListenerEventV2Async(ListenerEventV1 listenerEventV2);
        ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV2sAsync();
        ValueTask<ListenerEventV1> ModifyListenerEventV2Async(ListenerEventV1 listenerEventV2);
        ValueTask<ListenerEventV1> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id);
    }
}
