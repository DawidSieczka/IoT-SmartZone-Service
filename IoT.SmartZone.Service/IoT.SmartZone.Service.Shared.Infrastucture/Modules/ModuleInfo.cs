using System.Collections.Generic;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Modules;

public record ModuleInfo(string Name, IEnumerable<string> Policies);