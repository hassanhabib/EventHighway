// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using EventHighway.Core.Services.Foundations.EventAddresses.V2;
using Xeptions;

namespace EventHighway.Core.Clients.EventAddresses.V2
{
    internal class EventAddressesV2Client : IEventAddressesV2Client
    {
        private readonly IEventAddressV2Service eventAddressV2Service;

        public EventAddressesV2Client(IEventAddressV2Service eventAddressV2Service) =>
            this.eventAddressV2Service = eventAddressV2Service;

        public async ValueTask<EventAddressV2> RegisterEventAddressV2Async(EventAddressV2 eventAddressV2)
        {
            try
            {
                return await this.eventAddressV2Service.AddEventAddressV2Async(eventAddressV2);
            }
            catch (EventAddressV2ValidationException eventAddressV2ValidationException)
            {
                throw CreateEventAddressV2ClientDependencyValidationException(
                    eventAddressV2ValidationException.InnerException
                        as Xeption);
            }
            catch (EventAddressV2DependencyValidationException eventAddressV2DependencyValidationException)
            {
                throw CreateEventAddressV2ClientDependencyValidationException(
                    eventAddressV2DependencyValidationException.InnerException
                        as Xeption);
            }
            catch (EventAddressV2DependencyException eventV2DependencyException)
            {
                throw CreateEventAddressV2ClientDependencyException(
                    eventV2DependencyException.InnerException
                        as Xeption);
            }
            catch (EventAddressV2ServiceException eventV2ServiceException)
            {
                throw CreateEventAddressV2ClientServiceException(
                    eventV2ServiceException.InnerException
                        as Xeption);
            }
        }

        public async ValueTask<EventAddressV2> RemoveEventAddressV2ByIdAsync(Guid eventAddressV2Id)
        {
            try
            {
                return await this.eventAddressV2Service.RemoveEventAddressV2ByIdAsync(
                    eventAddressV2Id);
            }
            catch (EventAddressV2ValidationException eventAddressV2ValidationException)
            {
                throw CreateEventAddressV2ClientDependencyValidationException(
                    eventAddressV2ValidationException.InnerException
                        as Xeption);
            }
            catch (EventAddressV2DependencyValidationException eventAddressV2DependencyValidationException)
            {
                throw CreateEventAddressV2ClientDependencyValidationException(
                    eventAddressV2DependencyValidationException.InnerException
                        as Xeption);
            }
            catch (EventAddressV2DependencyException eventV2DependencyException)
            {
                throw CreateEventAddressV2ClientDependencyException(
                    eventV2DependencyException.InnerException
                        as Xeption);
            }
        }

        private static EventAddressV2ClientDependencyValidationException
            CreateEventAddressV2ClientDependencyValidationException(
                Xeption innerException)
        {
            return new EventAddressV2ClientDependencyValidationException(
                message: "Event address client validation error occurred, fix the errors and try again.",
                innerException: innerException);
        }

        private static EventAddressV2ClientDependencyException CreateEventAddressV2ClientDependencyException(
            Xeption innerException)
        {
            return new EventAddressV2ClientDependencyException(
                message: "Event address client dependency error occurred, contact support.",
                innerException: innerException);
        }

        private static EventAddressV2ClientServiceException CreateEventAddressV2ClientServiceException(
            Xeption innerException)
        {
            return new EventAddressV2ClientServiceException(
                message: "Event address client service error occurred, contact support.",
                innerException: innerException);
        }
    }
}
