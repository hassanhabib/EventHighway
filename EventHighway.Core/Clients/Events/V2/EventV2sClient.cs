// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Services.Coordinations.Events.V2;
using Xeptions;

namespace EventHighway.Core.Clients.Events.V2
{
    internal class EventV2sClient : IEventV2sClient
    {
        private readonly IEventV2CoordinationService eventV2CoordinationService;

        public EventV2sClient(IEventV2CoordinationService eventV2CoordinationService) =>
            this.eventV2CoordinationService = eventV2CoordinationService;

        public async ValueTask<EventV2> SubmitEventV2Async(EventV2 eventV2)
        {
            try
            {
                return await this.eventV2CoordinationService
                    .SubmitEventV2Async(eventV2);
            }
            catch (EventV2CoordinationDependencyValidationException
                eventV2CoordinationDependencyValidationException)
            {
                throw CreateEventV2ClientDependencyValidationException(
                    eventV2CoordinationDependencyValidationException.InnerException
                        as Xeption);
            }
            catch (EventV2CoordinationDependencyException
                eventV2CoordinationDependencyException)
            {
                throw CreateEventV2ClientDependencyException(
                    eventV2CoordinationDependencyException.InnerException
                        as Xeption);
            }
            catch (EventV2CoordinationServiceException
                eventV2CoordinationServiceException)
            {
                throw CreateEventV2ClientServiceException(
                    eventV2CoordinationServiceException.InnerException
                        as Xeption);
            }
        }

        public async ValueTask FireScheduledPendingEventV2sAsync()
        {
            try
            {
                await this.eventV2CoordinationService
                    .FireScheduledPendingEventV2sAsync();
            }
            catch (EventV2CoordinationDependencyValidationException
                eventV2CoordinationDependencyValidationException)
            {
                throw CreateEventV2ClientDependencyValidationException(
                    eventV2CoordinationDependencyValidationException.InnerException
                        as Xeption);
            }
            catch (EventV2CoordinationDependencyException
                eventV2CoordinationDependencyException)
            {
                throw CreateEventV2ClientDependencyException(
                    eventV2CoordinationDependencyException.InnerException
                        as Xeption);
            }
            catch (EventV2CoordinationServiceException
                eventV2CoordinationServiceException)
            {
                throw CreateEventV2ClientServiceException(
                    eventV2CoordinationServiceException.InnerException
                        as Xeption);
            }
        }

        public async ValueTask<EventV2> RemoveEventV2ByIdAsync(Guid eventV2Id)
        {
            try
            {
                return await this.eventV2CoordinationService
                    .RemoveEventV2ByIdAsync(eventV2Id);
            }
            catch (EventV2CoordinationDependencyValidationException
                eventV2CoordinationDependencyValidationException)
            {
                throw CreateEventV2ClientDependencyValidationException(
                    eventV2CoordinationDependencyValidationException.InnerException
                        as Xeption);
            }
            catch (EventV2CoordinationDependencyException
                eventV2CoordinationDependencyException)
            {
                throw CreateEventV2ClientDependencyException(
                    eventV2CoordinationDependencyException.InnerException
                        as Xeption);
            }
        }

        private static EventV2ClientDependencyValidationException CreateEventV2ClientDependencyValidationException(
            Xeption innerException)
        {
            return new EventV2ClientDependencyValidationException(
                message: "Event client validation error occurred, fix the errors and try again.",
                innerException: innerException);
        }

        private static EventV2ClientDependencyException CreateEventV2ClientDependencyException(
            Xeption innerException)
        {
            return new EventV2ClientDependencyException(
                message: "Event client dependency error occurred, contact support.",
                innerException: innerException);
        }

        private static EventV2ClientServiceException CreateEventV2ClientServiceException(
            Xeption innerException)
        {
            return new EventV2ClientServiceException(
                message: "Event client service error occurred, contact support.",
                innerException: innerException);
        }
    }
}
