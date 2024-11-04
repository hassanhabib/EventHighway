// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHighway.Core.Models.Events;

namespace EventHighway.Core.Services.Orchestrations.Events
{
    internal interface IEventOrchestrationService
    {
        ValueTask<Event> SubmitEventAsync(Event @event);
    }
}
