// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners;

namespace EventHighway.Core.Services.Foundations.EventListeners
{
    public interface IEventListenerService
    {
        ValueTask<EventListener> AddEventListenerAsync(EventListener eventListener);
        ValueTask<IQueryable<EventListener>> RetrieveAllEventListenersAsync();
    }
}
