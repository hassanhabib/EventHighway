// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.Clients.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.EventAddresses.V2
{
    public partial class EventAddressesV2ClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfDependencyValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            Guid someEventAddressV2Id = GetRandomId();

            var expectedEventAddressV2ClientDependencyValidationException =
                new EventAddressV2ClientDependencyValidationException(
                    message: "Event address client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventAddressV2ServiceMock.Setup(service =>
                service.RemoveEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventAddressV2> removeEventAddressV2ByIdTask =
                this.eventAddressesClient.RemoveEventAddressV2ByIdAsync(someEventAddressV2Id);

            EventAddressV2ClientDependencyValidationException
                actualEventAddressV2ClientDependencyValidationException =
                    await Assert.ThrowsAsync<EventAddressV2ClientDependencyValidationException>(
                        removeEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventAddressV2ClientDependencyValidationException);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.RemoveEventAddressV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
        }
    }
}
