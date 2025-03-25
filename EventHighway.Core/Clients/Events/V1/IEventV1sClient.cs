﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;

namespace EventHighway.Core.Clients.Events.V1
{
    public interface IEventV1sClient
    {
        ValueTask<EventV1> SubmitEventV1Async(EventV1 eventV1);
        ValueTask FireScheduledPendingEventV1sAsync();
        ValueTask<EventV1> RemoveEventV1ByIdAsync(Guid eventV1Id);
    }
}
