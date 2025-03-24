// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal interface IEventListenerV2Service
    {
        ValueTask<EventListenerV1> AddEventListenerV2Async(EventListenerV1 eventListenerV2);
        ValueTask<IQueryable<EventListenerV1>> RetrieveAllEventListenerV2sAsync();
        ValueTask<EventListenerV1> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id);
    }
}
