// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            EventListenerV2 someEventListenerV2 = CreateRandomEventListenerV2();
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
            ValueTask<EventListenerV2> addEventListenerV2Task =
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
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV2>()),
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
            EventListenerV2 someEventListenerV2 = CreateRandomEventListenerV2();
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
            ValueTask<EventListenerV2> addEventListenerV2Task =
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
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            EventListenerV2 someEventListenerV2 = CreateRandomEventListenerV2();
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
            ValueTask<EventListenerV2> addEventListenerV2Task =
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
                broker.InsertEventListenerV2Async(It.IsAny<EventListenerV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
