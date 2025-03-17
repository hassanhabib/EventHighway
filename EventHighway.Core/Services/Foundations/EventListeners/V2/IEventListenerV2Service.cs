// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal interface IEventListenerV2Service
    {
        ValueTask<EventListenerV2> AddEventListenerV2Async(EventListenerV2 eventListenerV2);
        ValueTask<IQueryable<EventListenerV2>> RetrieveAllEventListenerV2sAsync();
        ValueTask<EventListenerV2> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id);
    }
}
