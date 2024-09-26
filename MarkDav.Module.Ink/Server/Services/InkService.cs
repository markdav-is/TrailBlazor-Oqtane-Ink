using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using MarkDav.Module.Ink.Repository;

namespace MarkDav.Module.Ink.Services
{
    public class ServerInkService : IInkService
    {
        private readonly IInkRepository _InkRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerInkService(IInkRepository InkRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _InkRepository = InkRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.Ink>> GetInksAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_InkRepository.GetInks(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.Ink> GetInkAsync(int InkId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_InkRepository.GetInk(InkId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Get Attempt {InkId} {ModuleId}", InkId, ModuleId);
                return null;
            }
        }

        public Task<Models.Ink> AddInkAsync(Models.Ink Ink)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Ink.ModuleId, PermissionNames.Edit))
            {
                Ink = _InkRepository.AddInk(Ink);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Ink Added {Ink}", Ink);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Add Attempt {Ink}", Ink);
                Ink = null;
            }
            return Task.FromResult(Ink);
        }

        public Task<Models.Ink> UpdateInkAsync(Models.Ink Ink)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Ink.ModuleId, PermissionNames.Edit))
            {
                Ink = _InkRepository.UpdateInk(Ink);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Ink Updated {Ink}", Ink);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Update Attempt {Ink}", Ink);
                Ink = null;
            }
            return Task.FromResult(Ink);
        }

        public Task DeleteInkAsync(int InkId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _InkRepository.DeleteInk(InkId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Ink Deleted {InkId}", InkId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Delete Attempt {InkId} {ModuleId}", InkId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
