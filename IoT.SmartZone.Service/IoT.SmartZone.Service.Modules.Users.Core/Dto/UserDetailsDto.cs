namespace IoT.SmartZone.Service.Modules.Users.Core.Queries;

public class UserDetailsDto : UserDto
{
    public IEnumerable<string> Permissions { get; set; }
}