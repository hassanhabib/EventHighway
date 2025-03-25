// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V1
{
    internal interface IEventListenerV1OrchestrationService
    {
        ValueTask<EventListenerV1> AddEventListenerV1Async(EventListenerV1 eventListenerV1);
        ValueTask<IQueryable<EventListenerV1>> RetrieveEventListenerV1sByEventAddressIdAsync(Guid eventAddressId);
        ValueTask<EventListenerV1> RemoveEventListenerV1ByIdAsync(Guid eventListenerV1Id);
        ValueTask<ListenerEventV1> AddListenerEventV1Async(ListenerEventV1 listenerEventV1);
        ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV1sAsync();
        ValueTask<ListenerEventV1> ModifyListenerEventV1Async(ListenerEventV1 listenerEventV1);
        ValueTask<ListenerEventV1> RemoveListenerEventV1ByIdAsync(Guid listenerEventV1Id);
    }
}
