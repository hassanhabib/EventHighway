// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions;
using EventHighway.Core.Services.Orchestrations.EventListeners.V1;
using Xeptions;

namespace EventHighway.Core.Clients.EventListeners.V2
{
    internal class EventListenerV2sClient : IEventListenerV2sClient
    {
        private readonly IEventListenerV1OrchestrationService eventListenerV2OrchestrationService;

        public EventListenerV2sClient(IEventListenerV1OrchestrationService eventListenerV2OrchestrationService) =>
            this.eventListenerV2OrchestrationService = eventListenerV2OrchestrationService;

        public async ValueTask<EventListenerV1> RegisterEventListenerV2Async(
            EventListenerV1 eventListenerV2)
        {
            try
            {
                return await this.eventListenerV2OrchestrationService
                    .AddEventListenerV1Async(eventListenerV2);
            }
            catch (EventListenerV1OrchestrationValidationException
                eventListenerV2OrchestrationValidationException)
            {
                throw CreateEventListenerV2ClientDependencyValidationException(
                    eventListenerV2OrchestrationValidationException.InnerException
                        as Xeption);
            }
            catch (EventListenerV1OrchestrationDependencyValidationException
                eventListenerV2OrchestrationDependencyValidationException)
            {
                throw CreateEventListenerV2ClientDependencyValidationException(
                    eventListenerV2OrchestrationDependencyValidationException.InnerException
                        as Xeption);
            }
            catch (EventListenerV1OrchestrationDependencyException
                eventListenerV2OrchestrationDependencyException)
            {
                throw CreateEventListenerV2ClientDependencyException(
                    eventListenerV2OrchestrationDependencyException.InnerException
                        as Xeption);
            }
            catch (EventListenerV1OrchestrationServiceException
                eventListenerV2OrchestrationServiceException)
            {
                throw CreateEventListenerV2ClientServiceException(
                    eventListenerV2OrchestrationServiceException.InnerException
                        as Xeption);
            }
        }

        public async ValueTask<EventListenerV1> RemoveEventListenerV2ByIdAsync(
            Guid eventListenerV2Id)
        {
            try
            {
                return await this.eventListenerV2OrchestrationService
                    .RemoveEventListenerV1ByIdAsync(eventListenerV2Id);
            }
            catch (EventListenerV1OrchestrationValidationException
                eventListenerV2OrchestrationValidationException)
            {
                throw CreateEventListenerV2ClientDependencyValidationException(
                    eventListenerV2OrchestrationValidationException.InnerException
                        as Xeption);
            }
            catch (EventListenerV1OrchestrationDependencyValidationException
                eventListenerV2OrchestrationDependencyValidationException)
            {
                throw CreateEventListenerV2ClientDependencyValidationException(
                    eventListenerV2OrchestrationDependencyValidationException.InnerException
                        as Xeption);
            }
            catch (EventListenerV1OrchestrationDependencyException
                eventListenerV2OrchestrationDependencyException)
            {
                throw CreateEventListenerV2ClientDependencyException(
                    eventListenerV2OrchestrationDependencyException.InnerException
                        as Xeption);
            }
            catch (EventListenerV1OrchestrationServiceException
                eventListenerV2OrchestrationServiceException)
            {
                throw CreateEventListenerV2ClientServiceException(
                    eventListenerV2OrchestrationServiceException.InnerException
                        as Xeption);
            }
        }

        private static EventListenerV2ClientDependencyValidationException
            CreateEventListenerV2ClientDependencyValidationException(
                Xeption innerException)
        {
            return new EventListenerV2ClientDependencyValidationException(
                message: "Event listener client validation error occurred, fix the errors and try again.",
                innerException: innerException);
        }

        private static EventListenerV2ClientDependencyException CreateEventListenerV2ClientDependencyException(
            Xeption innerException)
        {
            return new EventListenerV2ClientDependencyException(
                message: "Event listener client dependency error occurred, contact support.",
                innerException: innerException);
        }

        private static EventListenerV2ClientServiceException CreateEventListenerV2ClientServiceException(
            Xeption innerException)
        {
            return new EventListenerV2ClientServiceException(
                message: "Event listener client service error occurred, contact support.",
                innerException: innerException);
        }
    }
}
