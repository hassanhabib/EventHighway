// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        [Theory]
        [MemberData(nameof(EventV2ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnRemoveByIdIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            Guid someEventV2Id = GetRandomId();

            var expectedEventV2CoordinationDependencyValidationException =
                new EventV2CoordinationDependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventV2OrchestrationServiceMock.Setup(service =>
                service.RemoveEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventV2> removeEventV2ByIdTask =
                this.eventV2CoordinationService.RemoveEventV2ByIdAsync(
                    someEventV2Id);

            EventV2CoordinationDependencyValidationException
                actualEventV2CoordinationDependencyValidationException =
                    await Assert.ThrowsAsync<EventV2CoordinationDependencyValidationException>(
                        removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2CoordinationDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2CoordinationDependencyValidationException);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RemoveEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationDependencyValidationException))),
                        Times.Once);

            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
