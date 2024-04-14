using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Features.Customers.Commands.Create;
using SampleApp.Application.Features.Customers.Commands.Delete;
using SampleApp.Application.Features.Customers.Commands.Update;
using SampleApp.Application.Features.Customers;
using SampleApp.Infrastructure.Data;
using SampleApp.Domain.Entities;

namespace Tests;

public class CustomerTests : IDisposable
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
    }

    public void Dispose()
    {
        if (_dbContext is not null)
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }

    [Fact]
    public async Task CreateCustomer_WhenValid_ResultIsSuccess()
    {
        // Arrange
        var command = new CreateCustomerCommand("New Customer", "00000 000000", "testing@domain.com");

        // Act
        var handler = new CreateCustomerCommandHandler(_dbContext);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var savedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(item => item.Id == result.Value);

        result.IsSuccessful.Should().BeTrue();
        savedCustomer.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateCustomer_WhenDuplicateName_ResultIsError()
    {
        // Arrange
        var existingName = "Customer A";
        await EnsureCustomerAsync(existingName, null, null);

        // Act
        var command = new CreateCustomerCommand(existingName, "00000 000000", "testing@domain.com");
        var handler = new CreateCustomerCommandHandler(_dbContext);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CustomerErrors.IsExisting(existingName));
    }

    [Fact]
    public async Task UpdateCustomer_WhenDuplicateName_ResultIsError()
    {
        // Arrange
        var existingName = "Customer A";
        await EnsureCustomerAsync(existingName, null, null);

        var newCustomer = await EnsureCustomerAsync("New Customer", null, null);

        // Act
        var command = new UpdateCustomerCommand(newCustomer.Id, existingName, null, null);
        var handler = new UpdateCustomerCommandHandler(_dbContext);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CustomerErrors.IsExisting(existingName));
    }

    [Fact]
    public async Task UpdateCustomer_WhenValid_ResultIsSuccess()
    {
        // Arrange
        var newCustomer = await EnsureCustomerAsync("New Customer", "123", "test@domain.com");

        var newName = "Test Customer";
        var newTelephone = "00000 000000";
        var newEmail = "chris@domain.com";

        // Act
        var command = new UpdateCustomerCommand(newCustomer.Id, newName, newTelephone, newEmail);
        var handler = new UpdateCustomerCommandHandler(_dbContext);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedCustomer = await _dbContext.Customers.FirstOrDefaultAsync(item => item.Id == newCustomer.Id);

        updatedCustomer.Should().NotBeNull();
        updatedCustomer!.Name.Should().Be(newName);
        updatedCustomer.Telephone.Should().Be(newTelephone);
        updatedCustomer.Email.Should().Be(newEmail);

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCustomer_WhenNotFound_ResultIsError()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var command = new DeleteCustomerCommand(customerId);

        // Act
        var handler = new DeleteCustomerCommandHandler(_dbContext);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CustomerErrors.NotFound(customerId));
    }

    [Fact]
    public async Task DeleteCustomer_WhenFound_ResultIsSuccess()
    {
        // Arrange
        var customer = await EnsureCustomerAsync("Existing", null, null);
        var command = new DeleteCustomerCommand(customer.Id);

        // Act
        var handler = new DeleteCustomerCommandHandler(_dbContext);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        var existingItem = await _dbContext.Customers.FirstOrDefaultAsync(item => item.Id == customer.Id);

        existingItem.Should().BeNull();
        result.IsSuccessful.Should().BeTrue();
    }

    private async Task<Customer> EnsureCustomerAsync(string name, string? telephone, string? email)
    {
        var customer = Customer.Create(name, telephone, email);
        _dbContext.Add(customer);
        await _dbContext.SaveChangesAsync();

        return customer;
    }
}