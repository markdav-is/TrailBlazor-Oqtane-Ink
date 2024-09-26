using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using MarkDav.Module.Lottie.Repository;
using System.Threading.Tasks;

namespace MarkDav.Module.Lottie.Manager
{
    public class LottieManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly ILottieRepository _LottieRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public LottieManager(ILottieRepository LottieRepository, IDBContextDependencies DBContextDependencies)
        {
            _LottieRepository = LottieRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new LottieContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new LottieContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.Lottie> Lotties = _LottieRepository.GetLotties(module.ModuleId).ToList();
            if (Lotties != null)
            {
                content = JsonSerializer.Serialize(Lotties);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Lottie> Lotties = null;
            if (!string.IsNullOrEmpty(content))
            {
                Lotties = JsonSerializer.Deserialize<List<Models.Lottie>>(content);
            }
            if (Lotties != null)
            {
                foreach(var Lottie in Lotties)
                {
                    _LottieRepository.AddLottie(new Models.Lottie { ModuleId = module.ModuleId, Name = Lottie.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var Lottie in _LottieRepository.GetLotties(pageModule.ModuleId))
           {
               if (Lottie.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "MarkDavLottie",
                       EntityId = Lottie.LottieId.ToString(),
                       Title = Lottie.Name,
                       Body = Lottie.Name,
                       ContentModifiedBy = Lottie.ModifiedBy,
                       ContentModifiedOn = Lottie.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
