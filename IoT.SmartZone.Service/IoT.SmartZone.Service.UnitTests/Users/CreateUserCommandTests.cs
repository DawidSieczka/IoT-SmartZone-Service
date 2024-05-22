using FluentAssertions;
using IoT.SmartZone.Service.Application.Operations.Users.Commands.CreateUser;
using IoT.SmartZone.Service.Application.Operations.Users.Commands.Responses;
using NSubstitute;

namespace IoT.SmartZone.Service.UnitTests.Users;

public class CreateUserCommandTests
{
    private ICreateUserCommandDataProvider _dataProviderSub;

    [SetUp]
    public void Setup()
    {
        _dataProviderSub = Substitute.For<ICreateUserCommandDataProvider>();
    }

    [Test]
    public async Task CreateUser_WithCorrectData_ShouldReturnNewUser()
    {
        var command = new CreateUserCommand()
        {
            Name = "John",
            LastName = "Smith",
            Email = "John@Smith.com"
        };

        var expectedUserEntityResult = new SmartZones.Domain.Entities.User()
        {
            Name = command.Name,
            LastName = command.LastName,
            Email = command.Email,
            ExternalId = Guid.NewGuid(),
            Id = 1
        };

        _dataProviderSub.CreateUserAsync(command).Returns(Task.FromResult(expectedUserEntityResult));

        var handler = new CreateUserCommandHandler(_dataProviderSub);

        var result = await handler.Handle(command,CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.LastName.Should().Be(command.LastName);
        result.Email.Should().Be(command.Email);
        result.ExternalId.Should().Be(expectedUserEntityResult.ExternalId);
    }
}