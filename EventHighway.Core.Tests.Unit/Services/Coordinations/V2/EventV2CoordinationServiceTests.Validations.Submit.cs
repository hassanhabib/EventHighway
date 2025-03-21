﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using FluentAssertions;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Coordinations.V2
{
    public partial class EventV2CoordinationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnSubmitIfEventV2IsNullAndLogItAsync()
        {
            // given
            EventV2 nullEventV2 = null;

            var nullEventV2CoordinationException =
                new NullEventV2CoordinationException(message: "Event is null.");

            var expectedEventV2CoordinationValidationException =
                new EventV2CoordinationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: nullEventV2CoordinationException);

            // when
            ValueTask<EventV2> submitEventV2Task =
                this.eventV2CoordinationService.SubmitEventV2Async(nullEventV2);

            EventV2CoordinationValidationException
                actualEventV2CoordinationValidationException =
                    await Assert.ThrowsAsync<EventV2CoordinationValidationException>(
                        submitEventV2Task.AsTask);

            // then
            actualEventV2CoordinationValidationException.Should().BeEquivalentTo(
                expectedEventV2CoordinationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2CoordinationValidationException))),
                        Times.Once);

            this.eventV2OrchestrationServiceMock.Verify(broker =>
                broker.SubmitEventV2Async(
                    It.IsAny<EventV2>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.RetrieveEventListenerV2sByEventAddressIdAsync(
                    It.IsAny<Guid>()),
                        Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.AddListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.eventV2OrchestrationServiceMock.Verify(service =>
                service.RunEventCallV2Async(
                    It.IsAny<EventCallV2>()),
                        Times.Never);

            this.eventListenerV2OrchestrationServiceMock.Verify(service =>
                service.ModifyListenerEventV2Async(
                    It.IsAny<ListenerEventV2>()),
                        Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.eventV2OrchestrationServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.eventListenerV2OrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
