// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;

namespace EventHighway.Core.Services.Foundations.EventCalls.V2
{
    internal interface IEventCallV2Service
    {
        ValueTask<EventCallV1> RunEventCallV2Async(EventCallV1 eventCallV2);
    }
}
