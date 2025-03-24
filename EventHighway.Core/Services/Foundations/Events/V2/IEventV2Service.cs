// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial interface IEventV2Service
    {
        ValueTask<EventV1> AddEventV2Async(EventV1 eventV2);
        ValueTask<IQueryable<EventV1>> RetrieveAllEventV2sAsync();
        ValueTask<EventV1> RemoveEventV2ByIdAsync(Guid eventV2Id);
    }
}
