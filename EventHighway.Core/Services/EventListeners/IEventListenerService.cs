// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;

namespace EventHighway.Core.Services.EventListeners
{
    internal interface IEventListenerService
    {
        ValueTask<EventListener> AddEventListenerAsync(EventListener eventListener);
        ValueTask<IQueryable<EventListener>> RetrieveAllEventListenersAsync();
    }
}
