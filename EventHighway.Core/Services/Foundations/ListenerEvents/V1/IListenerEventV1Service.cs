﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;

namespace EventHighway.Core.Services.Foundations.ListernEvents.V1
{
    internal interface IListenerEventV1Service
    {
        ValueTask<ListenerEventV1> AddListenerEventV1Async(ListenerEventV1 listenerEventV1);
        ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV1sAsync();
        ValueTask<ListenerEventV1> ModifyListenerEventV1Async(ListenerEventV1 listenerEventV1);
        ValueTask<ListenerEventV1> RemoveListenerEventV1ByIdAsync(Guid listenerEventV1Id);
    }
}
