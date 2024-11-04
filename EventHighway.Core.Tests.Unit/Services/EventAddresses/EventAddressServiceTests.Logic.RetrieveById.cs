// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventAddresses;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.EventAddresses
{
    public partial class EventAddressServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveEventAddressByIdAsync()
        {
            // given
            Guid randomEventAddressId = Guid.NewGuid();
            Guid inputEventAddressId = randomEventAddressId;

            EventAddress randomEventAddress =
                CreateRandomEventAddress();

            EventAddress selectedEventAddress =
                randomEventAddress;

            EventAddress expectedEventAddress =
                selectedEventAddress.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventAddressByIdAsync(inputEventAddressId))
                    .ReturnsAsync(selectedEventAddress);

            // when
            EventAddress actualEventAddress =
                await this.eventAddressService
                    .RetrieveEventAddressByIdAsync(inputEventAddressId);

            // then
            actualEventAddress.Should().BeEquivalentTo(expectedEventAddress);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventAddressByIdAsync(inputEventAddressId),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
