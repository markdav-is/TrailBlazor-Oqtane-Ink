using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Shared;
using System.Collections.Generic;

namespace MarkDav.Module.Ink
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Ink",
            Description = "Ink for Oqtane",
            Version = "1.0.0",
            ServerManagerType = "MarkDav.Module.Ink.Manager.InkManager, MarkDav.Module.Ink.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "MarkDav.Module.Ink.Shared.Oqtane",
            PackageName = "MarkDav.Module.Ink",
            Resources = new List<Resource>()
            {
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "~/Module.css" },
                new Resource { ResourceType = ResourceType.Script, Url = "~/Module.js" }
            }
        };
    }
}
