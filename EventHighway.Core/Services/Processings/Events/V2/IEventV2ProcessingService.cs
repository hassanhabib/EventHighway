// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Events.V2;

namespace EventHighway.Core.Services.Processings.Events.V2
{
    internal interface IEventV2ProcessingService
    {
        ValueTask<IQueryable<EventV2>> RetrieveScheduledPendingEventV2sAsync();
    }
}
