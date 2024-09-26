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
using MarkDav.Module.Ink.Repository;
using System.Threading.Tasks;

namespace MarkDav.Module.Ink.Manager
{
    public class InkManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IInkRepository _InkRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public InkManager(IInkRepository InkRepository, IDBContextDependencies DBContextDependencies)
        {
            _InkRepository = InkRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new InkContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new InkContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.Ink> Inks = _InkRepository.GetInks(module.ModuleId).ToList();
            if (Inks != null)
            {
                content = JsonSerializer.Serialize(Inks);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Ink> Inks = null;
            if (!string.IsNullOrEmpty(content))
            {
                Inks = JsonSerializer.Deserialize<List<Models.Ink>>(content);
            }
            if (Inks != null)
            {
                foreach(var Ink in Inks)
                {
                    _InkRepository.AddInk(new Models.Ink { ModuleId = module.ModuleId, Name = Ink.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var Ink in _InkRepository.GetInks(pageModule.ModuleId))
           {
               if (Ink.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "MarkDavInk",
                       EntityId = Ink.InkId.ToString(),
                       Title = Ink.Name,
                       Body = Ink.Name,
                       ContentModifiedBy = Ink.ModifiedBy,
                       ContentModifiedOn = Ink.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
