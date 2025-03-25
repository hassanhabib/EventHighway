﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;

namespace EventHighway.Core.Tests.Acceptance.Brokers
{
    public partial class ClientBroker
    {
        public async ValueTask<EventListenerV1> RegisterEventListenerV1Async(EventListenerV1 eventListenerV1) =>
            await this.eventHighwayClient.EventListenerV1s.RegisterEventListenerV1Async(eventListenerV1);

        public async ValueTask<EventListenerV1> RemoveEventListenerV1ByIdAsync(Guid eventListenerV1Id) =>
            await this.eventHighwayClient.EventListenerV1s.RemoveEventListenerV1ByIdAsync(eventListenerV1Id);
    }
}
