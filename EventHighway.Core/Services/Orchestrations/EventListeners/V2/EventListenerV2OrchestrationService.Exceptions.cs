// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Processings.EventListeners.V1.Exceptions;
using EventHighway.Core.Models.Services.Processings.ListenerEvents.V1.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal partial class EventListenerV2OrchestrationService
    {
        private delegate ValueTask<EventListenerV1> ReturningEventListenerV2Function();
        private delegate ValueTask<IQueryable<EventListenerV1>> ReturningEventListenerV2sFunction();
        private delegate ValueTask<ListenerEventV1> ReturningListenerEventV2Function();
        private delegate ValueTask<IQueryable<ListenerEventV1>> ReturningListenerEventV2sFunction();

        private async ValueTask<EventListenerV1> TryCatch(
            ReturningEventListenerV2Function returningEventListenerV2Function)
        {
            try
            {
                return await returningEventListenerV2Function();
            }
            catch (InvalidEventListenerV2OrchestrationException
                invalidEventListenerV2OrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventListenerV2OrchestrationException);
            }
            catch (NullEventListenerV2OrchestrationException
                nullEventListenerV2OrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    nullEventListenerV2OrchestrationException);
            }
            catch (EventListenerV1ProcessingValidationException
                eventListenerV1ProcessingValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventListenerV1ProcessingValidationException);
            }
            catch (EventListenerV1ProcessingDependencyValidationException
                eventListenerV1ProcessingDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventListenerV1ProcessingDependencyValidationException);
            }
            catch (EventListenerV1ProcessingDependencyException
                eventListenerV1ProcessingDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV1ProcessingDependencyException);
            }
            catch (EventListenerV1ProcessingServiceException
                eventListenerV1ProcessingServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV1ProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedEventListenerV2OrchestrationServiceException =
                    new FailedEventListenerV2OrchestrationServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV2OrchestrationServiceException);
            }
        }

        private async ValueTask<IQueryable<EventListenerV1>> TryCatch(
            ReturningEventListenerV2sFunction returningEventListenerV2sFunction)
        {
            try
            {
                return await returningEventListenerV2sFunction();
            }
            catch (InvalidEventListenerV2OrchestrationException
                invalidEventListenerV2OrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventListenerV2OrchestrationException);
            }
            catch (EventListenerV1ProcessingValidationException
                eventListenerV1ProcessingValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventListenerV1ProcessingValidationException);
            }
            catch (EventListenerV1ProcessingDependencyException
                eventListenerV1ProcessingDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV1ProcessingDependencyException);
            }
            catch (EventListenerV1ProcessingServiceException
                eventListenerV1ProcessingServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV1ProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedEventListenerV2OrchestrationServiceException =
                    new FailedEventListenerV2OrchestrationServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV2OrchestrationServiceException);
            }
        }

        private async ValueTask<ListenerEventV1> TryCatch(
            ReturningListenerEventV2Function returningListenerEventV2Function)
        {
            try
            {
                return await returningListenerEventV2Function();
            }
            catch (NullListenerEventV2OrchestrationException
                nullListenerEventV2OrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    nullListenerEventV2OrchestrationException);
            }
            catch (InvalidEventListenerV2OrchestrationException
                invalidEventListenerV2OrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventListenerV2OrchestrationException);
            }
            catch (ListenerEventV1ProcessingValidationException
                listenerEventV1ProcessingValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    listenerEventV1ProcessingValidationException);
            }
            catch (ListenerEventV1ProcessingDependencyValidationException
                listenerEventV1ProcessingDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    listenerEventV1ProcessingDependencyValidationException);
            }
            catch (ListenerEventV1ProcessingDependencyException
                listenerEventV1ProcessingDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    listenerEventV1ProcessingDependencyException);
            }
            catch (ListenerEventV1ProcessingServiceException
                listenerEventV1ProcessingServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    listenerEventV1ProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedEventListenerV2OrchestrationServiceException =
                    new FailedEventListenerV2OrchestrationServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV2OrchestrationServiceException);
            }
        }

        private async ValueTask<IQueryable<ListenerEventV1>> TryCatch(
            ReturningListenerEventV2sFunction returningListenerEventV2sFunction)
        {
            try
            {
                return await returningListenerEventV2sFunction();
            }
            catch (ListenerEventV1ProcessingDependencyException
                listenerEventV1ProcessingDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    listenerEventV1ProcessingDependencyException);
            }
            catch (ListenerEventV1ProcessingServiceException
                listenerEventV1ProcessingServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    listenerEventV1ProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedEventListenerV2OrchestrationServiceException =
                    new FailedEventListenerV2OrchestrationServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV2OrchestrationServiceException);
            }
        }

        private async ValueTask<EventListenerV2OrchestrationValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2OrchestrationValidationException =
                new EventListenerV2OrchestrationValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV2OrchestrationValidationException);

            return eventListenerV2OrchestrationValidationException;
        }

        private async ValueTask<EventListenerV2OrchestrationDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var eventListenerV2OrchestrationDependencyValidationException =
                new EventListenerV2OrchestrationDependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventListenerV2OrchestrationDependencyValidationException);

            return eventListenerV2OrchestrationDependencyValidationException;
        }

        private async ValueTask<EventListenerV2OrchestrationDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2OrchestrationDependencyException =
                new EventListenerV2OrchestrationDependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventListenerV2OrchestrationDependencyException);

            return eventListenerV2OrchestrationDependencyException;
        }

        private async ValueTask<EventListenerV2OrchestrationServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2OrchestrationServiceException =
                new EventListenerV2OrchestrationServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV2OrchestrationServiceException);

            return eventListenerV2OrchestrationServiceException;
        }
    }
}
