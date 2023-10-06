using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.Extensions.Logging;
using Moq;
using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Application.UseCases.Billings;
using StoneChallengeBillingApi.Domain.Interfaces.Repositories;
using StoneChallengeBillingApi.Domain.Records;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeBillingApi.Test.Unit.Fixtures;

namespace StoneChallengeBillingApi.Test.Unit.Application.UseCases;

[Collection(nameof(BillingCollection))]
public class ListBillingsUseCaseTest
{
    private readonly BillingTestFixture _fixture;


    public ListBillingsUseCaseTest(BillingTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "List Billings")]
    public async Task ItShouldBeAbleToListCreatedBillingsWhenAValidCpfIsPassed()
    {
        // Arrange
        var arr = _fixture.GenerateListQueryParametersDto(cpf: new Faker().Person.Cpf());
        var mockLogger = new Mock<ILogger<ListBillingsUseCase>>();
        var mockRepo = new Mock<IBillingsRepository>();
        
        mockRepo.Setup(x => x.ListAsync(It.IsAny<ListBillingsQueryParametersRecord>()))
            .ReturnsAsync(() => _fixture.GenerateValidBillingList(3));
        
        var sut = new ListBillingsUseCase(mockLogger.Object, mockRepo.Object);
        
        // Act
        var result = await sut.ExecuteAsync(arr);
        
        // Assert
        Assert.IsType<SuccessOperation<IEnumerable<ListBillingsResponseDTO>>>(result);
        Assert.NotEmpty(result.Data);
    }
    
    [Fact(DisplayName = "List Billings")]
    public async Task ItShouldBeAbleToListCreatedBillingsWhenAValidMonthIsPassed()
    {
        // Arrange
        var arr = _fixture.GenerateListQueryParametersDto("janeiro");
        var mockLogger = new Mock<ILogger<ListBillingsUseCase>>();
        var mockRepo = new Mock<IBillingsRepository>();
        
        mockRepo.Setup(x => x.ListAsync(It.IsAny<ListBillingsQueryParametersRecord>()))
            .ReturnsAsync(() => _fixture.GenerateValidBillingList(3));
        
        var sut = new ListBillingsUseCase(mockLogger.Object, mockRepo.Object);
        
        // Act
        var result = await sut.ExecuteAsync(arr);
        
        // Assert
        Assert.IsType<SuccessOperation<IEnumerable<ListBillingsResponseDTO>>>(result);
        Assert.NotEmpty(result.Data);
    }
    
    [Fact(DisplayName = "List Billings")]
    public async Task ItShouldNotBeAbleToListCreatedBillingsWhenNoParametersIsPassed()
    {
        // Arrange
        var arr = _fixture.GenerateListQueryParametersDto();
        var mockLogger = new Mock<ILogger<ListBillingsUseCase>>();
        var mockRepo = new Mock<IBillingsRepository>();
        var sut = new ListBillingsUseCase(mockLogger.Object, mockRepo.Object);
        
        // Act
        var result = await sut.ExecuteAsync(arr);
        
        // Assert
        Assert.IsType<FailedOperation<IEnumerable<ListBillingsResponseDTO>>>(result);
        Assert.Null(result.Data);
        Assert.Equal("Você precisa informar ao menos um CPF ou um mês de referência.", result.Reason);
    }
    
    [Fact(DisplayName = "List Billings")]
    public async Task ItShouldNotBeAbleToListCreatedBillingsWhenReferenceMonthIsInvalid()
    {
        // Arrange
        var arr = _fixture.GenerateListQueryParametersDto("invalid_month");
        var mockLogger = new Mock<ILogger<ListBillingsUseCase>>();
        var mockRepo = new Mock<IBillingsRepository>();
        var sut = new ListBillingsUseCase(mockLogger.Object, mockRepo.Object);
        
        // Act
        var result = await sut.ExecuteAsync(arr);
        
        // Assert
        Assert.IsType<FailedOperation<IEnumerable<ListBillingsResponseDTO>>>(result);
        Assert.Null(result.Data);
        Assert.Equal("Mês inválido. Opções disponíveis: Janeiro, Fevereiro, ...", result.Reason);
    }
    
    [Fact(DisplayName = "List Billings")]
    public async Task ItShouldNotBeAbleToListCreatedBillingsWhenCpfIsInvalid()
    {
        // Arrange
        var arr = _fixture.GenerateListQueryParametersDto(cpf: "invalid_cpf");
        var mockLogger = new Mock<ILogger<ListBillingsUseCase>>();
        var mockRepo = new Mock<IBillingsRepository>();
        var sut = new ListBillingsUseCase(mockLogger.Object, mockRepo.Object);
        
        // Act
        var result = await sut.ExecuteAsync(arr);
        
        // Assert
        Assert.IsType<FailedOperation<IEnumerable<ListBillingsResponseDTO>>>(result);
        Assert.Null(result.Data);
        Assert.Equal("CPF inválido.", result.Reason);
    }
}