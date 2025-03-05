// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.EventCall.V2.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.EventCalls.V2
{
    internal partial class EventCallV2Service
    {
        private delegate ValueTask<EventCallV2> ReturningEventCallV2Function();

        private async ValueTask<EventCallV2> TryCatch(ReturningEventCallV2Function returningEventCallV2Function)
        {
            try
            {
                return await returningEventCallV2Function();
            }
            catch (NullEventCallV2Exception nullEventCallV2Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(nullEventCallV2Exception);
            }
            catch (InvalidEventCallV2Exception invalidEventCallV2Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidEventCallV2Exception);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedEventCallV2ConfigurationException =
                    new FailedEventCallV2ConfigurationException(
                        message: "Failed event call configuration error occurred, contact support.",
                        innerException: httpResponseUrlNotFoundException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventCallV2ConfigurationException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedEventCallV2ConfigurationException =
                    new FailedEventCallV2ConfigurationException(
                        message: "Failed event call configuration error occurred, contact support.",
                        innerException: httpResponseUnauthorizedException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventCallV2ConfigurationException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedEventCallV2ConfigurationException =
                    new FailedEventCallV2ConfigurationException(
                        message: "Failed event call configuration error occurred, contact support.",
                        innerException: httpResponseForbiddenException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventCallV2ConfigurationException);
            }
            catch (HttpResponseMethodNotAllowedException httpResponseMethodNotAllowedException)
            {
                var failedEventCallV2ConfigurationException =
                    new FailedEventCallV2ConfigurationException(
                        message: "Failed event call configuration error occurred, contact support.",
                        innerException: httpResponseMethodNotAllowedException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventCallV2ConfigurationException);
            }
            catch (HttpResponseUnprocessableEntityException httpResponseUnprocessableEntityException)
            {
                var failedEventCallV2RequestException =
                    new FailedEventCallV2RequestException(
                        message: "Failed event call request error occurred, fix the errors and try again.",
                        innerException: httpResponseUnprocessableEntityException);

                throw await CreateAndLogDependencyValidationExceptionAsync(
                    failedEventCallV2RequestException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidEventCallV2Exception =
                    new InvalidEventCallV2Exception(
                        message: "Event call is invalid, fix the errors and try again.",
                        innerException: httpResponseBadRequestException,
                        data: httpResponseBadRequestException.Data);

                throw await CreateAndLogDependencyValidationExceptionAsync(
                    invalidEventCallV2Exception);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsEventCallV2Exception =
                    new AlreadyExistsEventCallV2Exception(
                        message: "Event call with same id already exists, try again.",
                        innerException: httpResponseConflictException);

                throw await CreateAndLogDependencyValidationExceptionAsync(
                    alreadyExistsEventCallV2Exception);
            }
            catch (HttpResponseFailedDependencyException httpResponseFailedDependencyException)
            {
                var invalidEventCallV2ReferenceException =
                    new InvalidEventCallV2ReferenceException(
                        message: "Invalid event call reference error occurred, fix the errors and try again.",
                        innerException: httpResponseFailedDependencyException);

                throw await CreateAndLogDependencyValidationExceptionAsync(
                    invalidEventCallV2ReferenceException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedEventCallV2DependencyException =
                    new FailedEventCallV2DependencyException(
                        message: "Failed event call dependency error occurred, contact support.",
                        innerException: httpResponseException);

                throw await CreateAndLogDependencyExceptionAsync(
                    failedEventCallV2DependencyException);
            }
            catch (Exception serviceException)
            {
                var failedEventCallV2ServiceException =
                    new FailedEventCallV2ServiceException(
                        message: "Failed event call service error occurred, contact support.",
                        innerException: serviceException);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventCallV2ServiceException);
            }
        }

        private async ValueTask<EventCallV2ValidationException> CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var eventCallV2ValidationException =
                new EventCallV2ValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventCallV2ValidationException);

            return eventCallV2ValidationException;
        }

        private async ValueTask<EventCallV2DependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var eventCallV2DependencyException =
                new EventCallV2DependencyException(
                    message: "Event call dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogCriticalAsync(eventCallV2DependencyException);

            return eventCallV2DependencyException;
        }

        private async ValueTask<EventCallV2DependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var eventCallV2DependencyValidationException =
                new EventCallV2DependencyValidationException(
                    message: "Event call validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventCallV2DependencyValidationException);

            return eventCallV2DependencyValidationException;
        }

        private async ValueTask<EventCallV2DependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventCallV2DependencyException =
                new EventCallV2DependencyException(
                    message: "Event call dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventCallV2DependencyException);

            return eventCallV2DependencyException;
        }

        private async ValueTask<EventCallV2ServiceException> CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var eventCallV2ServiceException =
                new EventCallV2ServiceException(
                    message: "Event call service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventCallV2ServiceException);

            return eventCallV2ServiceException;
        }
    }
}
