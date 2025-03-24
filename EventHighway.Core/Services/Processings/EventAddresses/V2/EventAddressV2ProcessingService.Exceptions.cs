// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions;
using EventHighway.Core.Models.Services.Processings.EventAddresses.V2.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Processings.EventAddresses.V2
{
    internal partial class EventAddressV2ProcessingService
    {
        private delegate ValueTask<EventAddressV1> ReturningEventAddressV2Function();

        private async ValueTask<EventAddressV1> TryCatch(
            ReturningEventAddressV2Function returningEventAddressV2Function)
        {
            try
            {
                return await returningEventAddressV2Function();
            }
            catch (InvalidEventAddressV2ProcessingException
                invalidEventAddressV2ProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventAddressV2ProcessingException);
            }
            catch (EventAddressV1ValidationException
                eventAddressV2ValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventAddressV2ValidationException);
            }
            catch (EventAddressV1DependencyValidationException
                eventAddressV2DependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventAddressV2DependencyValidationException);
            }
            catch (EventAddressV1DependencyException
                eventAddressV2DependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventAddressV2DependencyException);
            }
            catch (EventAddressV1ServiceException
                eventAddressV2ServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventAddressV2ServiceException);
            }
            catch (Exception exception)
            {
                var failedEventAddressV2ProcessingServiceException =
                    new FailedEventAddressV2ProcessingServiceException(
                        message: "Failed event address service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventAddressV2ProcessingServiceException);
            }
        }

        private async ValueTask<EventAddressV2ProcessingValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventAddressV2ProcessingValidationException =
                new EventAddressV2ProcessingValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV2ProcessingValidationException);

            return eventAddressV2ProcessingValidationException;
        }

        private async ValueTask<EventAddressV2ProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var eventAddressV2ProcessingDependencyValidationException =
                new EventAddressV2ProcessingDependencyValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventAddressV2ProcessingDependencyValidationException);

            return eventAddressV2ProcessingDependencyValidationException;
        }

        private async ValueTask<EventAddressV2ProcessingDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventAddressV2ProcessingDependencyException =
                new EventAddressV2ProcessingDependencyException(
                    message: "Event address dependency error occurred, contact support.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventAddressV2ProcessingDependencyException);

            return eventAddressV2ProcessingDependencyException;
        }

        private async ValueTask<EventAddressV2ProcessingServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventAddressV2ProcessingServiceException =
                new EventAddressV2ProcessingServiceException(
                    message: "Event address service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV2ProcessingServiceException);

            return eventAddressV2ProcessingServiceException;
        }
    }
}
