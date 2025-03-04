// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.EventListeners.V2.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            SqlException sqlException = GetSqlException();

            var failedEventListenerV2StorageException =
                new FailedEventListenerV2StorageException(
                    message: "Failed event listener storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventListenerV2DependencyException =
                new EventListenerV2DependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: failedEventListenerV2StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEventListenerV2sAsync())
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<IQueryable<EventListenerV2>> retrieveAllEventListenerV2sTask =
                this.eventListenerV2Service.RetrieveAllEventListenerV2sAsync();

            EventListenerV2DependencyException actualEventListenerV2DependencyException =
                await Assert.ThrowsAsync<EventListenerV2DependencyException>(
                    retrieveAllEventListenerV2sTask.AsTask);

            // then
            actualEventListenerV2DependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEventListenerV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfExceptionOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedEventListenerV2ServiceException =
                new FailedEventListenerV2ServiceException(
                    message: "Failed event listener service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventListenerV2ServiceException =
                new EventListenerV2ServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: failedEventListenerV2ServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEventListenerV2sAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<IQueryable<EventListenerV2>> retrieveAllEventListenerV2sTask =
                this.eventListenerV2Service.RetrieveAllEventListenerV2sAsync();

            EventListenerV2ServiceException actualEventListenerV2ServiceException =
                await Assert.ThrowsAsync<EventListenerV2ServiceException>(
                    retrieveAllEventListenerV2sTask.AsTask);

            // then
            actualEventListenerV2ServiceException.Should()
                .BeEquivalentTo(expectedEventListenerV2ServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEventListenerV2sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2ServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
