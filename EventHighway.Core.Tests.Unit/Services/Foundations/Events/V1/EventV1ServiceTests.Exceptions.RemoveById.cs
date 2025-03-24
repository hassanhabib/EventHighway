﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Foundations.Events.V1.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V1
{
    public partial class EventV1ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someEventV1Id = GetRandomId();
            SqlException sqlException = GetSqlException();

            var failedEventV1StorageException =
                new FailedEventV1StorageException(
                    message: "Failed event storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventV1DependencyException =
                new EventV1DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: failedEventV1StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventV1> removeEventV1ByIdTask =
                this.eventV1Service.RemoveEventV1ByIdAsync(
                    someEventV1Id);

            EventV1DependencyException actualEventV1DependencyException =
                await Assert.ThrowsAsync<EventV1DependencyException>(
                    removeEventV1ByIdTask.AsTask);

            // then
            actualEventV1DependencyException.Should()
                .BeEquivalentTo(expectedEventV1DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventV1DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationErrorOnRemoveByIdIfDbUpdateConcurrencyErrorAndLogItAsync()
        {
            // given
            Guid someEventV1Id = GetRandomId();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedEventV1Exception =
                new LockedEventV1Exception(
                    message: "Event is locked, try again.",
                    innerException: dbUpdateConcurrencyException);

            var expectedEventV1DependencyValidationException =
                new EventV1DependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: lockedEventV1Exception);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            // when
            ValueTask<EventV1> removeEventV1ByIdTask =
                this.eventV1Service.RemoveEventV1ByIdAsync(
                    someEventV1Id);

            EventV1DependencyValidationException actualEventV1DependencyValidationException =
                await Assert.ThrowsAsync<EventV1DependencyValidationException>(
                    removeEventV1ByIdTask.AsTask);

            // then
            actualEventV1DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV1DependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV1ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            Guid someEventV1Id = GetRandomId();
            var dbUpdateException = new DbUpdateException();

            var failedEventV1StorageException =
                new FailedEventV1StorageException(
                    message: "Failed event storage error occurred, contact support.",
                    innerException: dbUpdateException);

            var expectedEventV1DependencyException =
                new EventV1DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: failedEventV1StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateException);

            // when
            ValueTask<EventV1> removeEventV1ByIdTask =
                this.eventV1Service.RemoveEventV1ByIdAsync(someEventV1Id);

            EventV1DependencyException actualEventV1DependencyException =
                await Assert.ThrowsAsync<EventV1DependencyException>(
                    removeEventV1ByIdTask.AsTask);

            // then
            actualEventV1DependencyException.Should()
                .BeEquivalentTo(expectedEventV1DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV1ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfExceptionOccursAndLogItAsync()
        {
            // given
            Guid someEventV1Id = GetRandomId();
            var serviceException = new Exception();

            var failedEventV1ServiceException =
                new FailedEventV1ServiceException(
                    message: "Failed event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventV1ServiceException =
                new EventV1ServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: failedEventV1ServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventV1> removeEventV1ByIdTask =
                this.eventV1Service.RemoveEventV1ByIdAsync(
                    someEventV1Id);

            EventV1ServiceException actualEventV1ServiceException =
                await Assert.ThrowsAsync<EventV1ServiceException>(
                    removeEventV1ByIdTask.AsTask);

            // then
            actualEventV1ServiceException.Should()
                .BeEquivalentTo(expectedEventV1ServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV1ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1ServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
