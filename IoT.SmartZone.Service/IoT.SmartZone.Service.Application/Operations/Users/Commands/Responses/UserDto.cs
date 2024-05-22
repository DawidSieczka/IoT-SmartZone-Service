namespace IoT.SmartZone.Service.Application.Operations.Users.Commands.Responses;

public class UserDto
{
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}