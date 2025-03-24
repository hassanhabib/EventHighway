// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            EventListenerV1 someEventListenerV2 = CreateRandomEventListenerV2();
            SqlException sqlException = GetSqlException();

            var failedEventListenerV2StorageException =
                new FailedEventListenerV2StorageException(
                    message: "Failed event listener storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventListenerV2DependencyException =
                new EventListenerV2DependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: failedEventListenerV2StorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(someEventListenerV2);

            EventListenerV2DependencyException actualEventListenerV2DependencyException =
                await Assert.ThrowsAsync<EventListenerV2DependencyException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2DependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfEventListenerV2AlreadyExistsAndLogItAsync()
        {
            // given
            string randomMessage = GetRandomString();
            EventListenerV1 someEventListenerV2 = CreateRandomEventListenerV2();
            var duplicateKeyException = new DuplicateKeyException(randomMessage);

            var alreadyExistsEventListenerV2Exception =
                new AlreadyExistsEventListenerV2Exception(
                    message: "Event listener with the same id already exists.",
                    innerException: duplicateKeyException);

            var expectedEventListenerV2DependencyValidationException =
                new EventListenerV2DependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: alreadyExistsEventListenerV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(someEventListenerV2);

            EventListenerV2DependencyValidationException actualEventListenerV2DependencyValidationException =
                await Assert.ThrowsAsync<EventListenerV2DependencyValidationException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            EventListenerV1 someEventListenerV2 = CreateRandomEventListenerV2();
            string someMessage = GetRandomString();

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(someMessage);

            var invalidEventListenerV2ReferenceException =
                new InvalidEventListenerV2ReferenceException(
                    message: "Invalid event listener reference error occurred.",
                    innerException: foreignKeyConstraintConflictException);

            var expectedEventListenerV2DependencyValidationException =
                new EventListenerV2DependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: invalidEventListenerV2ReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(foreignKeyConstraintConflictException);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(someEventListenerV2);

            EventListenerV2DependencyValidationException actualEventListenerV2DependencyValidationException =
                await Assert.ThrowsAsync<EventListenerV2DependencyValidationException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDbUpdateExceptionOccursAndLogItAsync()
        {
            // given
            EventListenerV1 someEventListenerV2 = CreateRandomEventListenerV2();
            var dbUpdateException = new DbUpdateException();

            var failedEventListenerV2StorageException =
                new FailedEventListenerV2StorageException(
                    message: "Failed event listener storage error occurred, contact support.",
                    innerException: dbUpdateException);

            var expectedEventListenerV2DependencyException =
                new EventListenerV2DependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: failedEventListenerV2StorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(dbUpdateException);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(someEventListenerV2);

            EventListenerV2DependencyException actualEventListenerV2DependencyException =
                await Assert.ThrowsAsync<EventListenerV2DependencyException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2DependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfExceptionOccursAndLogItAsync()
        {
            // given
            EventListenerV1 someEventListenerV2 = CreateRandomEventListenerV2();
            var serviceException = new Exception();

            var failedEventListenerV2ServiceException =
                new FailedEventListenerV2ServiceException(
                    message: "Failed event listener service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventListenerV2ServiceException =
                new EventListenerV2ServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: failedEventListenerV2ServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventListenerV1> addEventListenerV2Task =
                this.eventListenerV2Service.AddEventListenerV2Async(someEventListenerV2);

            EventListenerV2ServiceException actualEventListenerV2ServiceException =
                await Assert.ThrowsAsync<EventListenerV2ServiceException>(
                    addEventListenerV2Task.AsTask);

            // then
            actualEventListenerV2ServiceException.Should()
                .BeEquivalentTo(expectedEventListenerV2ServiceException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ServiceException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV1>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
