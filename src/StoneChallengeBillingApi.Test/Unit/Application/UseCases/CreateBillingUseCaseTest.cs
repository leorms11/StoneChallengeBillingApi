using Microsoft.Extensions.Logging;
using Moq;
using StoneChallengeBillingApi.Application.UseCases.Billings;
using StoneChallengeBillingApi.Domain.Interfaces.Services;
using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeBillingApi.Test.Unit.Fixtures;

namespace StoneChallengeBillingApi.Test.Unit.Application.UseCases;

[Collection(nameof(BillingCollection))]
public class CreateBillingUseCaseTest
{
    private readonly BillingTestFixture _fixture;


    public CreateBillingUseCaseTest(BillingTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Create a Valid Billing")]
    public async Task ItShouldBeAbleToCreateABilling()
    {
        // Arrange
        var arr = _fixture.GenerateValidCreateRequestDto();
        var mockService = new Mock<IBillingService>();
        var mockLogger = new Mock<ILogger<CreateBillingUseCase>>();
        
        mockService.Setup(x => x.CreateAsync(It.IsAny<Billing>()))
            .ReturnsAsync(() => _fixture.GenerateSuccessResultFromBilling());

        var sut = new CreateBillingUseCase(mockLogger.Object, mockService.Object);
        
        // Act
        var result = await sut.ExecuteAsync(arr);
        
        // Assert
        Assert.IsType<SuccessOperation>(result);
    }
    
    [Fact(DisplayName = "Create an Invalid Billing")]
    public async Task ItShouldNotBeAbleToCreateABillingWithAInvalidRequestDto()
    {
        // Arrange
        var arr = _fixture.GenerateInvalidCreateRequestDto();
        var mockService = new Mock<IBillingService>();
        var mockLogger = new Mock<ILogger<CreateBillingUseCase>>();
        var sut = new CreateBillingUseCase(mockLogger.Object, mockService.Object);

        // Act
        var result = await sut.ExecuteAsync(arr);
        
        // Assert
        Assert.IsType<FailedOperation>(result);
    }
    
    [Fact(DisplayName = "Create an Invalid Billing")]
    public async Task ItShouldNotBeAbleToCreateABillingWhenEntityIsInvalid()
    {
        // Arrange
        var arr = _fixture.GenerateValidCreateRequestDto();
        var arr2 = _fixture.GenerateInvalidBilling();
        var mockService = new Mock<IBillingService>();
        var mockLogger = new Mock<ILogger<CreateBillingUseCase>>();
        
        mockService.Setup(x => x.CreateAsync(It.IsAny<Billing>()))
            .ReturnsAsync(() => _fixture.GenerateFailedResultFromBilling(arr2.Notifications));

        var sut = new CreateBillingUseCase(mockLogger.Object, mockService.Object);
        
        // Act
        var result = await sut.ExecuteAsync(arr);
        
        // Assert
        Assert.IsType<FailedOperation>(result);
        Assert.NotNull(result.Errors);
        Assert.NotEmpty(result.Errors.Fields);
    }
}