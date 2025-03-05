// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners.V2;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal interface IEventListenerV2Service
    {
        ValueTask<IQueryable<EventListenerV2>> RetrieveAllEventListenerV2sAsync();
    }
}
