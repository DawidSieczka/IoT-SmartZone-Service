namespace IoT.SmartZone.Service.Modules.Users.Core.Dto;
internal class RoleDto
{
    public string Name { get; set; }
    
    public IEnumerable<string> Permissions { get; set; }
    
}
