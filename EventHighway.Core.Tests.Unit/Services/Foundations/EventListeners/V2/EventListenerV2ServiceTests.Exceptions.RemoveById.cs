// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someEventListenerV2Id = GetRandomId();
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
                broker.SelectEventListenerV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventListenerV2> removeEventListenerV2ByIdTask =
                this.eventListenerV2Service.RemoveEventListenerV2ByIdAsync(
                    someEventListenerV2Id);

            EventListenerV2DependencyException actualEventListenerV2DependencyException =
                await Assert.ThrowsAsync<EventListenerV2DependencyException>(
                    removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2DependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventListenerV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
