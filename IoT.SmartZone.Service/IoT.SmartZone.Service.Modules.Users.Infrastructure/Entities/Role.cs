namespace IoT.SmartZone.Service.Modules.Users.Infrastructure.Entities;

public class Role
{
    public string Name { get; set; }
    public IEnumerable<string> Permissions { get; set; }
    public virtual IEnumerable<User> Users { get; set; }

    public static string Default => User;
    public const string User = "user";
    public const string Admin = "admin";
}
