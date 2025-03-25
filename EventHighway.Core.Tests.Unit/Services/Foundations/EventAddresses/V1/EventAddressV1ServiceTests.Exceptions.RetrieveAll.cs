// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventAddresses.V1
{
    public partial class EventAddressV1ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            SqlException sqlException = CreateSqlException();

            var failedEventAddressV1StorageException =
                new FailedEventAddressV1StorageException(
                    message: "Failed event address storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventAddressV1DependencyException =
                new EventAddressV1DependencyException(
                    message: "Event address dependency error occurred, contact support.",
                    innerException: failedEventAddressV1StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEventAddressV1sAsync())
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<IQueryable<EventAddressV1>> retrieveAllEventAddressV1sTask =
                this.eventAddressV1Service.RetrieveAllEventAddressV1sAsync();

            EventAddressV1DependencyException actualEventAddressV1DependencyException =
                await Assert.ThrowsAsync<EventAddressV1DependencyException>(
                    retrieveAllEventAddressV1sTask.AsTask);

            // then
            actualEventAddressV1DependencyException.Should()
                .BeEquivalentTo(expectedEventAddressV1DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEventAddressV1sAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV1DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
