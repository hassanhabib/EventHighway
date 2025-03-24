﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.EventListeners.V1
{
    internal partial class EventListenerV1Service
    {
        private delegate ValueTask<EventListenerV1> ReturningEventListenerV1Function();
        private delegate ValueTask<IQueryable<EventListenerV1>> ReturningEventListenerV1sFunction();

        private async ValueTask<IQueryable<EventListenerV1>> TryCatch(
            ReturningEventListenerV1sFunction returningEventListenerV1sFunction)
        {
            try
            {
                return await returningEventListenerV1sFunction();
            }
            catch (SqlException sqlException)
            {
                var failedEventListenerV1StorageException =
                    new FailedEventListenerV1StorageException(
                        message: "Failed event listener storage error occurred, contact support.",
                        innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventListenerV1StorageException);
            }
            catch (Exception serviceException)
            {
                var failedEventListenerV1ServiceException =
                    new FailedEventListenerV1ServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: serviceException);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV1ServiceException);
            }
        }

        private async ValueTask<EventListenerV1> TryCatch(
            ReturningEventListenerV1Function returningEventListenerV1Function)
        {
            try
            {
                return await returningEventListenerV1Function();
            }
            catch (InvalidEventListenerV1Exception invalidEventListenerV1Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventListenerV1Exception);
            }
            catch (NullEventListenerV1Exception nullEventListenerV1Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    nullEventListenerV1Exception);
            }
            catch (NotFoundEventListenerV1Exception notFoundEventListenerV1Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    notFoundEventListenerV1Exception);
            }
            catch (SqlException sqlException)
            {
                var failedEventListenerV1StorageException =
                    new FailedEventListenerV1StorageException(
                        message: "Failed event listener storage error occurred, contact support.",
                        innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventListenerV1StorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsEventListenerV1Exception =
                    new AlreadyExistsEventListenerV1Exception(
                        message: "Event listener with the same id already exists.",
                        innerException: duplicateKeyException);

                throw await CreateAndLogDependencyValidationExceptionAsync(
                    alreadyExistsEventListenerV1Exception);
            }
            catch (ForeignKeyConstraintConflictException
                foreignKeyConstraintConflictException)
            {
                var invalidEventListenerV1ReferenceException =
                    new InvalidEventListenerV1ReferenceException(
                        message: "Invalid event listener reference error occurred.",
                        innerException: foreignKeyConstraintConflictException);

                throw await CreateAndLogDependencyValidationExceptionAsync(
                    invalidEventListenerV1ReferenceException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedEventListenerV1Exception =
                    new LockedEventListenerV1Exception(
                        message: "Event listener is locked, try again.",
                        innerException: dbUpdateConcurrencyException);

                throw await CreateAndLogDependencyValidationExceptionAsync(lockedEventListenerV1Exception);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedEventListenerV1StorageException =
                    new FailedEventListenerV1StorageException(
                        message: "Failed event listener storage error occurred, contact support.",
                        innerException: dbUpdateException);

                throw await CreateAndLogDependencyExceptionAsync(failedEventListenerV1StorageException);
            }
            catch (Exception serviceException)
            {
                var failedEventListenerV1ServiceException =
                    new FailedEventListenerV1ServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: serviceException);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV1ServiceException);
            }
        }

        private async ValueTask<EventListenerV1ValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventListenerV1ValidationException =
                new EventListenerV1ValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV1ValidationException);

            return eventListenerV1ValidationException;
        }

        private async ValueTask<EventListenerV1DependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var eventListenerV1DependencyValidationException =
                new EventListenerV1DependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV1DependencyValidationException);

            return eventListenerV1DependencyValidationException;
        }

        private async ValueTask<EventListenerV1DependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventListenerV1DependencyException =
                new EventListenerV1DependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV1DependencyException);

            return eventListenerV1DependencyException;
        }

        private async ValueTask<EventListenerV1DependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var eventListenerV1DependencyException =
                new EventListenerV1DependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogCriticalAsync(eventListenerV1DependencyException);

            return eventListenerV1DependencyException;
        }

        private async ValueTask<EventListenerV1ServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventListenerV1ServiceException =
                new EventListenerV1ServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV1ServiceException);

            return eventListenerV1ServiceException;
        }
    }
}
