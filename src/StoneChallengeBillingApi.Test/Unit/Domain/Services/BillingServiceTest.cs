using Moq;
using StoneChallengeBillingApi.Domain.Interfaces.Repositories;
using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Domain.Services;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeBillingApi.Test.Unit.Fixtures;

namespace StoneChallengeBillingApi.Test.Unit.Domain.Services;

[Collection(nameof(BillingCollection))]
public class BillingServiceTest
{
    private readonly BillingTestFixture _fixture;


    public BillingServiceTest(BillingTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Create a Valid Billing")]
    public async Task ItShouldBeAbleToCreateABilling()
    {
        // Arrange
        var arr = _fixture.GenerateValidBilling();
        var mockRepo = new Mock<IBillingsRepository>();
        
        mockRepo.Setup(x => x.CreateAsync(It.IsAny<Billing>()))
            .ReturnsAsync(() => arr);

        var sut = new BillingService(mockRepo.Object);
        
        // Act
        var result = await sut.CreateAsync(arr);
        
        // Assert
        Assert.Equal(result.Data.CustomerCpf, arr.CustomerCpf);
    }
    
    [Fact(DisplayName = "Create a Invalid Billing")]
    public async Task ItShouldNotBeAbleToCreateABillingWithInvalidProps()
    {
        // Arrange
        var arr = _fixture.GenerateInvalidBilling();
        var mockRepo = new Mock<IBillingsRepository>();

        var sut = new BillingService(mockRepo.Object);
        
        // Act
        var result = await sut.CreateAsync(arr);
        
        // Assert
        Assert.IsType<FailedOperation<Billing>>(result);
    }
}