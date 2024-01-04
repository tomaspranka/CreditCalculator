using CreditCalculator.Before;
using FluentAssertions;

namespace CreditCalculator.Tests;

public class CustomerServiceBeforeTests
{
    public const int Regular = 1;
    public const int ImportantClient = 2;
    public const int VeryImportantClient = 3;

    [Theory]
    [InlineData("", "last", "test@test.com")]
    [InlineData("first", "", "test@test.com")]
    [InlineData("first", "last", "")]
    [InlineData("first", "last", "testtest.com")]
    [InlineData("first", "last", "test@testcom")]

    public void AddCustomer_ReturnsFalse_WhenArgumentsAreInvalid(
        string firstName,
        string lastName,
        string email)
    {
        //Arrange
        var customerService = new CustomerService();

        //Act
        var result = customerService.AddCustomer(firstName, lastName, email, DateTime.Now, Regular);
        //Assert 
        result.Should().BeFalse();
    }

    [Fact]
    public void AddCustomer_ReturnsFalse_WhenDateOfBirthIsInvalid()
    {
        //Arrange
        var customerService = new CustomerService();
        var invalidDateOfBirth = DateTime.Now.AddYears(-20);

        //Act
        var result = customerService.AddCustomer("first", "last", "test@test.com", invalidDateOfBirth, Regular);
        //Assert 
        result.Should().BeFalse();
    }

    [Fact]
    public void AddCustomer_ReturnsTrue_WhenVeryImportantClient()
    {
        //Arrange
        var customerService = new CustomerService();        

        //Act
        var result = customerService.AddCustomer("first", "last", "test@test.com", DateTime.Now.AddYears(-40) , VeryImportantClient);
        //Assert 
        result.Should().BeTrue();
    }

    [Fact]
    public void AddCustomer_ReturnsFalse_WhenImportantClientAndUnder40Years()
    {
        //Arrange
        var customerService = new CustomerService();

        //Act
        var result = customerService.AddCustomer("first", "last", "test@test.com", DateTime.Now.AddYears(-22), ImportantClient);
        //Assert 
        result.Should().BeFalse();
    }
    [Fact]
    public void AddCustomer_ReturnsTrue_WhenVeryImportantClientAndOver40Years()
    {
        //Arrange
        var customerService = new CustomerService();

        //Act
        var result = customerService.AddCustomer("first", "last", "test@test.com", DateTime.Now.AddYears(-45), ImportantClient);

        //Assert 
        result.Should().BeTrue();
    }

    [Fact]
    public void AddCustomer_ReturnsFalse_WhenRegularClientAndOver40Years()
    {
        //Arrange
        var customerService = new CustomerService();

        //Act
        var result = customerService.AddCustomer("first", "last", "test@test.com", DateTime.Now.AddYears(-30), Regular);
        //Assert 
        result.Should().BeFalse();
    }

    

    [Theory]
    [InlineData(Regular)]
    [InlineData(ImportantClient)]
    [InlineData(VeryImportantClient)]
    public void AddCustomer_ReturnsTrue_WhenJohnDoe(int companyId)
    {
        //Arrange
        var customerService = new CustomerService();
        
        //Act
        var result = customerService.AddCustomer("John", "Doe", "test@test.com", DateTime.Now.AddYears(-45), companyId);
        
        //Assert 
        result.Should().BeTrue();
    }
}