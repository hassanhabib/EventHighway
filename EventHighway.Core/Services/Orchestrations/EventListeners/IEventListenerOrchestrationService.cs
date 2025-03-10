// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events;

namespace EventHighway.Core.Services.Orchestrations.EventListeners
{
    internal interface IEventListenerOrchestrationService
    {
        ValueTask<Event> SubmitEventToListenersAsync(Event @event);
    }
}
