// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events;

namespace EventHighway.Core.Services.Coordinations.Events
{
    internal interface IEventCoordinationService
    {
        ValueTask<Event> SubmitEventAsync(Event @event);
    }
}
