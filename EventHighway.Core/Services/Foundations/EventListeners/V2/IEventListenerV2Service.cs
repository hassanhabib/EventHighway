// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal interface IEventListenerV2Service
    {
        ValueTask<IQueryable<EventListenerV2>> RetrieveAllEventListenerV2sAsync();
        ValueTask<EventListenerV2> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id);
    }
}
