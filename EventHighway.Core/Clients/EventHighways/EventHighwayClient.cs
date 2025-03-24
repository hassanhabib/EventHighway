// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using EventHighway.Core.Brokers.Apis;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Clients.EventAddresses;
using EventHighway.Core.Clients.EventAddresses.V2;
using EventHighway.Core.Clients.EventListeners;
using EventHighway.Core.Clients.EventListeners.V2;
using EventHighway.Core.Clients.Events;
using EventHighway.Core.Clients.Events.V2;
using EventHighway.Core.Clients.ListenerEvents.V2;
using EventHighway.Core.Services.Coordinations.Events;
using EventHighway.Core.Services.Coordinations.Events.V2;
using EventHighway.Core.Services.Foundations.EventAddresses;
using EventHighway.Core.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Services.Foundations.EventCalls;
using EventHighway.Core.Services.Foundations.EventCalls.V1;
using EventHighway.Core.Services.Foundations.EventListeners;
using EventHighway.Core.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Services.Foundations.Events;
using EventHighway.Core.Services.Foundations.Events.V1;
using EventHighway.Core.Services.Foundations.ListernEvents;
using EventHighway.Core.Services.Foundations.ListernEvents.V1;
using EventHighway.Core.Services.Orchestrations.EventListeners;
using EventHighway.Core.Services.Orchestrations.EventListeners.V2;
using EventHighway.Core.Services.Orchestrations.Events;
using EventHighway.Core.Services.Orchestrations.Events.V2;
using EventHighway.Core.Services.Processings.EventAddresses.V1;
using EventHighway.Core.Services.Processings.EventCalls;
using EventHighway.Core.Services.Processings.EventCalls.V2;
using EventHighway.Core.Services.Processings.EventListeners;
using EventHighway.Core.Services.Processings.EventListeners.V2;
using EventHighway.Core.Services.Processings.Events.V2;
using EventHighway.Core.Services.Processings.ListenerEvents;
using EventHighway.Core.Services.Processings.ListenerEvents.V2;
using Microsoft.Extensions.DependencyInjection;

namespace EventHighway.Core.Clients.EventHighways
{
    public class EventHighwayClient : IEventHighwayClient
    {
        private readonly string dataConnectionString;

        public EventHighwayClient(string dataConnectionString)
        {
            this.dataConnectionString = dataConnectionString;
            IServiceProvider serviceProvider = ConfigureDependencies();
            InitializeClients(serviceProvider);
        }

        public IEventAddressesClient EventAddresses { get; set; }
        public IEventListenersClient EventListeners { get; set; }
        public IEventsClient Events { get; set; }
        public IEventV2sClient EventV2s { get; set; }
        public IEventAddressesV2Client IEventAddressV2s { get; set; }
        public IEventListenerV2sClient EventListenerV2s { get; set; }
        public IListenerEventV2sClient ListenerEventV2s { get; set; }

        private void InitializeClients(IServiceProvider serviceProvider)
        {
            this.EventAddresses =
                serviceProvider.GetRequiredService<IEventAddressesClient>();

            this.EventListeners =
                serviceProvider.GetRequiredService<IEventListenersClient>();

            this.Events =
                serviceProvider.GetRequiredService<IEventsClient>();

            this.EventV2s =
                serviceProvider.GetRequiredService<IEventV2sClient>();

            this.IEventAddressV2s =
                serviceProvider.GetRequiredService<IEventAddressesV2Client>();

            this.EventListenerV2s =
                serviceProvider.GetRequiredService<IEventListenerV2sClient>();

            this.ListenerEventV2s =
                serviceProvider.GetRequiredService<IListenerEventV2sClient>();
        }

        private IServiceProvider ConfigureDependencies()
        {
            var serviceCollection =
                new ServiceCollection();

            RegisterBrokers(serviceCollection);
            RegisterFoundationServices(serviceCollection);
            RegisterProcessingServices(serviceCollection);
            RegisterOrchestrationServices(serviceCollection);
            RegisterCoordinationServices(serviceCollection);
            RegisterClients(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private void RegisterBrokers(IServiceCollection services)
        {
            services.AddLogging();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            services.AddDbContext<StorageBroker>();

            services.AddTransient<
                IStorageBroker,
                StorageBroker>(broker =>
                    new StorageBroker(this.dataConnectionString));

            services.AddTransient<IApiBroker, ApiBroker>();
        }

        private static void RegisterFoundationServices(IServiceCollection services)
        {
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IEventAddressService, EventAddressService>();
            services.AddTransient<IEventListenerService, EventListenerService>();
            services.AddTransient<IListenerEventService, ListenerEventService>();
            services.AddTransient<IEventCallService, EventCallService>();
            services.AddTransient<IEventV1Service, EventV1Service>();
            services.AddTransient<IEventListenerV1Service, EventListenerV1Service>();
            services.AddTransient<IListenerEventV1Service, ListenerEventV1Service>();
            services.AddTransient<IEventCallV1Service, EventCallV1Service>();
            services.AddTransient<IEventAddressV1Service, EventAddressV1Service>();
        }

        private static void RegisterProcessingServices(IServiceCollection services)
        {
            services.AddTransient<
                IEventListenerProcessingService,
                EventListenerProcessingService>();

            services.AddTransient<
                IEventCallProcessingService,
                EventCallProcessingService>();

            services.AddTransient<
                IListenerEventProcessingService,
                ListenerEventProcessingService>();

            services.AddTransient<
                IEventCallV2ProcessingService,
                EventCallV2ProcessingService>();

            services.AddTransient<
                IEventListenerV2ProcessingService,
                EventListenerV2ProcessingService>();

            services.AddTransient<
                IEventV2ProcessingService,
                EventV2ProcessingService>();

            services.AddTransient<
                IListenerEventV2ProcessingService,
                ListenerEventV2ProcessingService>();

            services.AddTransient<
                IEventAddressV1ProcessingService,
                EventAddressV1ProcessingService>();
        }

        private static void RegisterOrchestrationServices(IServiceCollection services)
        {
            services.AddTransient<
                IEventListenerOrchestrationService,
                EventListenerOrchestrationService>();

            services.AddTransient<
                IEventOrchestrationService,
                EventOrchestrationService>();

            services.AddTransient<
                IEventListenerV2OrchestrationService,
                EventListenerV2OrchestrationService>();

            services.AddTransient<
                IEventV2OrchestrationService,
                EventV2OrchestrationService>();
        }

        private static void RegisterCoordinationServices(IServiceCollection services)
        {
            services.AddTransient<
                IEventCoordinationService,
                EventCoordinationService>();

            services.AddTransient<
                IEventV2CoordinationService,
                EventV2CoordinationService>();
        }

        private static void RegisterClients(IServiceCollection services)
        {
            services.AddTransient<
                IEventsClient,
                EventsClient>();

            services.AddTransient<
                IEventV2sClient,
                EventV2sClient>();

            services.AddTransient<
                IEventListenersClient,
                EventListenersClient>();

            services.AddTransient<
                IEventListenerV2sClient,
                EventListenerV2sClient>();

            services.AddTransient<
                IEventAddressesClient,
                EventAddressesClient>();

            services.AddTransient<
                IEventAddressesV2Client,
                EventAddressesV2Client>();

            services.AddTransient<
                IListenerEventV2sClient,
                ListenerEventV2sClient>();

            services.AddTransient<
                IEventHighwayClient,
                EventHighwayClient>();
        }
    }
}