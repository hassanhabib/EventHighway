// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;

namespace EventHighway.Core.Clients.EventListeners
{
    public interface IEventListenersClient
    {
        ValueTask<EventListener> RegisterEventListenerAsync(EventListener eventListener);
        ValueTask<IQueryable<EventListener>> GetAllEventListenersAsync();
    }
}
