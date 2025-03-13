// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V2;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial interface IEventV2Service
    {
        ValueTask<EventV2> AddEventV2Async(EventV2 eventV2);
        ValueTask<IQueryable<EventV2>> RetrieveAllEventV2sAsync();
        ValueTask<EventV2> RemoveEventV2ByIdAsync(Guid eventV2Id);
    }
}
