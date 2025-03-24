// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;

namespace EventHighway.Core.Services.Foundations.EventListeners.V1
{
    internal interface IEventListenerV1Service
    {
        ValueTask<EventListenerV1> AddEventListenerV1Async(EventListenerV1 eventListenerV1);
        ValueTask<IQueryable<EventListenerV1>> RetrieveAllEventListenerV1sAsync();
        ValueTask<EventListenerV1> RemoveEventListenerV1ByIdAsync(Guid eventListenerV1Id);
    }
}
