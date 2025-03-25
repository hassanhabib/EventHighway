﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.EventAddresses.V1
{
    internal partial class EventAddressV1Service
    {
        private delegate ValueTask<EventAddressV1> ReturningEventAddressV1Function();
        private delegate ValueTask<IQueryable<EventAddressV1>> ReturningEventAddressV1sFunction();

        private async ValueTask<EventAddressV1> TryCatch(
            ReturningEventAddressV1Function returningEventAddressV1Function)
        {
            try
            {
                return await returningEventAddressV1Function();
            }
            catch (NullEventAddressV1Exception nullEventAddressV1Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    nullEventAddressV1Exception);
            }
            catch (InvalidEventAddressV1Exception invalidEventAddressV1Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventAddressV1Exception);
            }
            catch (NotFoundEventAddressV1Exception notFoundEventAddressV1Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    notFoundEventAddressV1Exception);
            }
            catch (SqlException sqlException)
            {
                var failedEventAddressV1StorageException =
                    new FailedEventAddressV1StorageException(
                        message: "Failed event address storage error occurred, contact support.",
                        innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventAddressV1StorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsEventAddressV1Exception =
                    new AlreadyExistsEventAddressV1Exception(
                        message: "Event address with the same id already exists.",
                        innerException: duplicateKeyException);

                throw await CreateAndLogDependencyValidationExceptionAsync(
                    alreadyExistsEventAddressV1Exception);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedEventAddressV1Exception =
                    new LockedEventAddressV1Exception(
                        message: "Event address is locked, try again.",
                        innerException: dbUpdateConcurrencyException);

                throw await CreateAndLogDependencyValidationExceptionAsync(lockedEventAddressV1Exception);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedEventAddressV1StorageException =
                    new FailedEventAddressV1StorageException(
                        message: "Failed event address storage error occurred, contact support.",
                        innerException: dbUpdateException);

                throw await CreateAndLogDependencyExceptionAsync(failedEventAddressV1StorageException);
            }
            catch (Exception serviceException)
            {
                var failedEventAddressV1ServiceException =
                    new FailedEventAddressV1ServiceException(
                        message: "Failed event address service error occurred, contact support.",
                        innerException: serviceException);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventAddressV1ServiceException);
            }
        }

        private async ValueTask<IQueryable<EventAddressV1>> TryCatch(
            ReturningEventAddressV1sFunction returningEventAddressV1sFunction)
        {
            try
            {
                return await returningEventAddressV1sFunction();
            }
            catch (SqlException sqlException)
            {
                var failedEventAddressV1StorageException =
                    new FailedEventAddressV1StorageException(
                        message: "Failed event address storage error occurred, contact support.",
                        innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventAddressV1StorageException);
            }
            catch (Exception serviceException)
            {
                var failedEventAddressV1ServiceException =
                    new FailedEventAddressV1ServiceException(
                        message: "Failed event address service error occurred, contact support.",
                        innerException: serviceException);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventAddressV1ServiceException);
            }
        }

        private async ValueTask<EventAddressV1ValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventAddressV1ValidationException =
                new EventAddressV1ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV1ValidationException);

            return eventAddressV1ValidationException;
        }

        private async ValueTask<EventAddressV1DependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var eventAddressV1DependencyValidationException =
                new EventAddressV1DependencyValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV1DependencyValidationException);

            return eventAddressV1DependencyValidationException;
        }

        private async ValueTask<EventAddressV1DependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var eventAddressV1DependencyException =
                new EventAddressV1DependencyException(
                    message: "Event address dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogCriticalAsync(eventAddressV1DependencyException);

            return eventAddressV1DependencyException;
        }

        private async ValueTask<EventAddressV1DependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventAddressV1DependencyException =
                new EventAddressV1DependencyException(
                    message: "Event address dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV1DependencyException);

            return eventAddressV1DependencyException;
        }

        private async ValueTask<EventAddressV1ServiceException> CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var eventAddressV1ServiceException =
                new EventAddressV1ServiceException(
                    message: "Event address service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV1ServiceException);

            return eventAddressV1ServiceException;
        }
    }
}
