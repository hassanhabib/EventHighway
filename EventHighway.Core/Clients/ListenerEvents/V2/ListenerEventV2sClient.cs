// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.ListenerEvents.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using EventHighway.Core.Services.Orchestrations.EventListeners.V2;
using Xeptions;

namespace EventHighway.Core.Clients.ListenerEvents.V2
{
    internal class ListenerEventV2sClient : IListenerEventV2sClient
    {
        private readonly IEventListenerV2OrchestrationService eventListenerV2OrchestrationService;

        public ListenerEventV2sClient(IEventListenerV2OrchestrationService eventListenerV2OrchestrationService) =>
            this.eventListenerV2OrchestrationService = eventListenerV2OrchestrationService;

        public async ValueTask<IQueryable<ListenerEventV2>> RetrieveAllListenerEventV2sAsync()
        {
            try
            {
                return await this.eventListenerV2OrchestrationService
                    .RetrieveAllListenerEventV2sAsync();
            }
            catch (EventListenerV2OrchestrationDependencyValidationException
                eventListenerV2OrchestrationDependencyValidationException)
            {
                throw CreateListenerEventV2ClientDependencyValidationException(
                    eventListenerV2OrchestrationDependencyValidationException.InnerException
                        as Xeption);
            }
            catch (EventListenerV2OrchestrationDependencyException
                eventListenerV2OrchestrationDependencyException)
            {
                throw CreateListenerEventV2ClientDependencyException(
                    eventListenerV2OrchestrationDependencyException.InnerException
                        as Xeption);
            }
            catch (EventListenerV2OrchestrationServiceException
                eventListenerV2OrchestrationServiceException)
            {
                throw CreateListenerEventV2ClientServiceException(
                    eventListenerV2OrchestrationServiceException.InnerException
                        as Xeption);
            }
        }

        public async ValueTask<ListenerEventV2> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id)
        {
            return await this.eventListenerV2OrchestrationService.RemoveListenerEventV2ByIdAsync(
                listenerEventV2Id);
        }

        private static ListenerEventV2ClientDependencyValidationException
            CreateListenerEventV2ClientDependencyValidationException(
                Xeption innerException)
        {
            return new ListenerEventV2ClientDependencyValidationException(
                message: "Listener event client validation error occurred, fix the errors and try again.",
                innerException: innerException);
        }

        private static ListenerEventV2ClientDependencyException CreateListenerEventV2ClientDependencyException(
            Xeption innerException)
        {
            return new ListenerEventV2ClientDependencyException(
                message: "Listener event client dependency error occurred, contact support.",
                innerException: innerException);
        }

        private static ListenerEventV2ClientServiceException CreateListenerEventV2ClientServiceException(
            Xeption innerException)
        {
            return new ListenerEventV2ClientServiceException(
                message: "Listener event client service error occurred, contact support.",
                innerException: innerException);
        }
    }
}
