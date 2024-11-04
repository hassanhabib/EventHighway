// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events;

namespace EventHighway.Core.Clients.Events
{
    public interface IEventsClient
    {
        ValueTask<Event> SubmitEventAsync(Event @event);
    }
}
