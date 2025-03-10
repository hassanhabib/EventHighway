// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial class EventV2Service
    {
        private delegate ValueTask<EventV2> ReturningEventV2Function();
        private delegate ValueTask<IQueryable<EventV2>> ReturningEventV2sFunction();

        private async ValueTask<EventV2> TryCatch(ReturningEventV2Function returningEventV2Function)
        {
            try
            {
                return await returningEventV2Function();
            }
            catch (NullEventV2Exception nullEventV2Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(nullEventV2Exception);
            }
            catch (InvalidEventV2Exception invalidEventV2Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidEventV2Exception);
            }
            catch (SqlException sqlException)
            {
                var failedEventV2StorageException =
                    new FailedEventV2StorageException(
                        message: "Failed event storage error occurred, contact support.",
                        innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventV2StorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsEventV2Exception =
                    new AlreadyExistsEventV2Exception(
                        message: "Event with the same id already exists.",
                        innerException: duplicateKeyException);

                throw await CreateAndLogDependencyValidationExceptionAsync(
                    alreadyExistsEventV2Exception);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidEventV2ReferenceException =
                    new InvalidEventV2ReferenceException(
                        message: "Invalid event reference error occurred.",
                        innerException: foreignKeyConstraintConflictException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidEventV2ReferenceException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedEventV2StorageException =
                    new FailedEventV2StorageException(
                        message: "Failed event storage error occurred, contact support.",
                        innerException: dbUpdateException);

                throw await CreateAndLogDependencyExceptionAsync(failedEventV2StorageException);
            }
            catch (Exception serviceException)
            {
                var failedEventV2ServiceException =
                    new FailedEventV2ServiceException(
                        message: "Failed event service error occurred, contact support.",
                        innerException: serviceException);

                throw await CreateAndLogServiceExceptionAsync(failedEventV2ServiceException);
            }
        }

        private async ValueTask<IQueryable<EventV2>> TryCatch(ReturningEventV2sFunction returningEventV2sFunction)
        {
            try
            {
                return await returningEventV2sFunction();
            }
            catch (SqlException sqlException)
            {
                var failedEventV2StorageException =
                    new FailedEventV2StorageException(
                        message: "Failed event storage error occurred, contact support.",
                        innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedEventV2StorageException);
            }
            catch (Exception serviceException)
            {
                var failedEventV2ServiceException =
                    new FailedEventV2ServiceException(
                        message: "Failed event service error occurred, contact support.",
                        innerException: serviceException);

                throw await CreateAndLogServiceExceptionAsync(failedEventV2ServiceException);
            }
        }

        private async ValueTask<EventV2ValidationException> CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var eventV2ValidationException =
                new EventV2ValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2ValidationException);

            return eventV2ValidationException;
        }

        private async ValueTask<EventV2DependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var eventV2DependencyException =
                new EventV2DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogCriticalAsync(eventV2DependencyException);

            return eventV2DependencyException;
        }

        private async ValueTask<EventV2DependencyValidationException> CreateAndLogDependencyValidationExceptionAsync(
            Xeption exception)
        {
            var eventV2DependencyValidationException =
                new EventV2DependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2DependencyValidationException);

            return eventV2DependencyValidationException;
        }

        private async ValueTask<EventV2DependencyException> CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var eventV2DependencyException =
                new EventV2DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2DependencyException);

            return eventV2DependencyException;
        }

        private async ValueTask<EventV2ServiceException> CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var eventV2ServiceException =
                new EventV2ServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2ServiceException);

            return eventV2ServiceException;
        }
    }
}
