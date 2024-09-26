using Oqtane.Models;
using Oqtane.Modules;

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
            PackageName = "MarkDav.Module.Lottie" 
        };
    }
}
