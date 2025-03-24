﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
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
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            EventV1 someEventV1 = CreateRandomEventV1();
            SqlException sqlException = GetSqlException();

            var failedEventV1StorageException =
                new FailedEventV1StorageException(
                    message: "Failed event storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventV1DependencyException =
                new EventV1DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: failedEventV1StorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventV1> addEventV1Task =
                this.eventV1Service.AddEventV1Async(someEventV1);

            EventV1DependencyException actualEventV1DependencyException =
                await Assert.ThrowsAsync<EventV1DependencyException>(
                    addEventV1Task.AsTask);

            // then
            actualEventV1DependencyException.Should()
                .BeEquivalentTo(expectedEventV1DependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventV1DependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfEventV1AlreadyExistsAndLogItAsync()
        {
            // given
            string randomMessage = GetRandomString();
            EventV1 someEventV1 = CreateRandomEventV1();
            var duplicateKeyException = new DuplicateKeyException(randomMessage);

            var alreadyExistsEventV1Exception =
                new AlreadyExistsEventV1Exception(
                    message: "Event with the same id already exists.",
                    innerException: duplicateKeyException);

            var expectedEventV1DependencyValidationException =
                new EventV1DependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: alreadyExistsEventV1Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<EventV1> addEventV1Task =
                this.eventV1Service.AddEventV1Async(someEventV1);

            EventV1DependencyValidationException actualEventV1DependencyValidationException =
                await Assert.ThrowsAsync<EventV1DependencyValidationException>(
                    addEventV1Task.AsTask);

            // then
            actualEventV1DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV1DependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            EventV1 someEventV1 = CreateRandomEventV1();
            string someMessage = GetRandomString();

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(someMessage);

            var invalidEventV1ReferenceException =
                new InvalidEventV1ReferenceException(
                    message: "Invalid event reference error occurred.",
                    innerException: foreignKeyConstraintConflictException);

            var expectedEventV1DependencyValidationException =
                new EventV1DependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: invalidEventV1ReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(foreignKeyConstraintConflictException);

            // when
            ValueTask<EventV1> addEventV1Task =
                this.eventV1Service.AddEventV1Async(someEventV1);

            EventV1DependencyValidationException actualEventV1DependencyValidationException =
                await Assert.ThrowsAsync<EventV1DependencyValidationException>(
                    addEventV1Task.AsTask);

            // then
            actualEventV1DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV1DependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDbUpdateExceptionOccursAndLogItAsync()
        {
            // given
            EventV1 someEventV1 = CreateRandomEventV1();
            var dbUpdateException = new DbUpdateException();

            var failedEventV1StorageException =
                new FailedEventV1StorageException(
                    message: "Failed event storage error occurred, contact support.",
                    innerException: dbUpdateException);

            var expectedEventV1DependencyException =
                new EventV1DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: failedEventV1StorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(dbUpdateException);

            // when
            ValueTask<EventV1> addEventV1Task =
                this.eventV1Service.AddEventV1Async(someEventV1);

            EventV1DependencyException actualEventV1DependencyException =
                await Assert.ThrowsAsync<EventV1DependencyException>(
                    addEventV1Task.AsTask);

            // then
            actualEventV1DependencyException.Should()
                .BeEquivalentTo(expectedEventV1DependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1DependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfExceptionOccursAndLogItAsync()
        {
            // given
            EventV1 someEventV1 = CreateRandomEventV1();
            var serviceException = new Exception();

            var failedEventV1ServiceException =
                new FailedEventV1ServiceException(
                    message: "Failed event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventV1ServiceException =
                new EventV1ServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: failedEventV1ServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventV1> addEventV1Task =
                this.eventV1Service.AddEventV1Async(someEventV1);

            EventV1ServiceException actualEventV1ServiceException =
                await Assert.ThrowsAsync<EventV1ServiceException>(
                    addEventV1Task.AsTask);

            // then
            actualEventV1ServiceException.Should()
                .BeEquivalentTo(expectedEventV1ServiceException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1ServiceException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV1Async(It.IsAny<EventV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
