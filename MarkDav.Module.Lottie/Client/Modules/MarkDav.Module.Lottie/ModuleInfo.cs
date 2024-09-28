using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Shared;
using System.Collections.Generic;

namespace MarkDav.Module.Lottie
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Lottie",
            Description = "animations",
            Version = "1.0.0",
            ServerManagerType = "MarkDav.Module.Lottie.Manager.LottieManager, MarkDav.Module.Lottie.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "MarkDav.Module.Lottie.Shared.Oqtane",
            PackageName = "MarkDav.Module.Lottie",
            Resources = new List<Resource>()
            {
                new Resource { ResourceType = ResourceType.Stylesheet, Url = "~/Module.css" },
                new Resource { ResourceType = ResourceType.Script, Url = "~/Module.js" },

                //load the lottie-web componwnt library
                new Resource { ResourceType = ResourceType.Script, 
                    ES6Module=true, Location=ResourceLocation.Body,
                    Url = "https://unpkg.com/@lottiefiles/dotlottie-wc@latest/dist/dotlottie-wc.js" }
            }
        };
    }
}
