// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall.V2;

namespace EventHighway.Core.Services.Processings.EventCalls.V2
{
    internal interface IEventCallV2ProcessingService
    {
        ValueTask<EventCallV2> RunAsync(EventCallV2 eventCallV2);
    }
}
