// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events.V2;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial interface IEventV2Service
    {
        ValueTask<EventV2> AddEventV2Async(EventV2 eventV2);
    }
}
