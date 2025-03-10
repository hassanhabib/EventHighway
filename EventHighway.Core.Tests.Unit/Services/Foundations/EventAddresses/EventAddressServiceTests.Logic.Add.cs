// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.EventAddresses
{
    public partial class EventAddressServiceTests
    {
        [Fact]
        public async Task ShouldAddEventAddressAsync()
        {
            // given
            EventAddress randomEventAddress =
                CreateRandomEventAddress();

            EventAddress inputEventAddress =
                randomEventAddress;

            EventAddress insertedEventAddress =
                inputEventAddress;

            EventAddress expectedEventAddress =
                insertedEventAddress.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventAddressAsync(inputEventAddress))
                    .ReturnsAsync(insertedEventAddress);

            // when
            EventAddress actualEventAddress =
                await this.eventAddressService.AddEventAddressAsync(
                    inputEventAddress);

            // then
            actualEventAddress.Should().BeEquivalentTo(
                expectedEventAddress);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressAsync(inputEventAddress),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
