// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Brokers.Apis;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Clients.EventAddresses;
using EventHighway.Core.Clients.EventListeners;
using EventHighway.Core.Clients.Events;
using EventHighway.Core.Services.Coordinations.Events;
using EventHighway.Core.Services.Foundations.EventAddresses;
using EventHighway.Core.Services.Foundations.EventCalls;
using EventHighway.Core.Services.Foundations.EventListeners;
using EventHighway.Core.Services.Foundations.Events;
using EventHighway.Core.Services.Foundations.ListernEvents;
using EventHighway.Core.Services.Orchestrations.EventListeners;
using EventHighway.Core.Services.Orchestrations.Events;
using EventHighway.Core.Services.Processings.EventCalls;
using EventHighway.Core.Services.Processings.EventListeners;
using EventHighway.Core.Services.Processings.ListenerEvents;
using Microsoft.Extensions.DependencyInjection;

namespace EventHighway.Core.Clients.EventHighways
{
    public class EventHighwayClient : IEventHighwayClient
    {
        private readonly string dataConnectionString;

        public EventHighwayClient(string dataConnectionString)
        {
            this.dataConnectionString = dataConnectionString;
            IServiceProvider serviceProvider = RegisterServices();
            InitializeClients(serviceProvider);
        }

        public IEventAddressesClient EventAddresses { get; set; }
        public IEventListenersClient EventListeners { get; set; }
        public IEventsClient Events { get; set; }

        private void InitializeClients(IServiceProvider serviceProvider)
        {
            this.EventAddresses =
                serviceProvider.GetRequiredService<IEventAddressesClient>();

            this.EventListeners =
                serviceProvider.GetRequiredService<IEventListenersClient>();

            this.Events =
                serviceProvider.GetRequiredService<IEventsClient>();
        }

        private IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<IDateTimeBroker, DateTimeBroker>()

                .AddDbContext<StorageBroker>()

                .AddTransient<IStorageBroker, StorageBroker>(broker =>
                    new StorageBroker(this.dataConnectionString))

                .AddTransient<ILoggingBroker, LoggingBroker>()

                .AddTransient<IApiBroker, ApiBroker>()
                .AddTransient<IEventService, EventService>()

                .AddTransient<
                    IEventAddressService,
                    EventAddressService>()

                .AddTransient<
                    IEventListenerService,
                    EventListenerService>()

                .AddTransient<
                    IListenerEventService,
                    ListenerEventService>()

                .AddTransient<IEventCallService, EventCallService>()

                .AddTransient<
                    IEventListenerProcessingService,
                    EventListenerProcessingService>()

                .AddTransient<
                    IEventCallProcessingService,
                    EventCallProcessingService>()

                .AddTransient<
                    IListenerEventProcessingService,
                    ListenerEventProcessingService>()

                .AddTransient<
                    IEventListenerOrchestrationService,
                    EventListenerOrchestrationService>()

                .AddTransient<
                    IEventOrchestrationService,
                    EventOrchestrationService>()

                .AddTransient<
                    IEventCoordinationService,
                    EventCoordinationService>()

                .AddTransient<IEventsClient, EventsClient>()
                .AddTransient<IEventListenersClient, EventListenersClient>()
                .AddTransient<IEventAddressesClient, EventAddressesClient>()
                .AddTransient<IEventHighwayClient, EventHighwayClient>();

            IServiceProvider serviceProvider =
                serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}