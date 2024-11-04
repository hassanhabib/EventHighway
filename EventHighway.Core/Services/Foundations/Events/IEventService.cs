// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events;

namespace EventHighway.Core.Services.Foundations.Events
{
    internal interface IEventService
    {
        ValueTask<Event> AddEventAsync(Event @event);
    }
}
