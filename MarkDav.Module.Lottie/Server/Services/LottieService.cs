using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using MarkDav.Module.Lottie.Repository;

namespace MarkDav.Module.Lottie.Services
{
    public class ServerLottieService : ILottieService
    {
        private readonly ILottieRepository _LottieRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerLottieService(ILottieRepository LottieRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _LottieRepository = LottieRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.Lottie>> GetLottiesAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_LottieRepository.GetLotties(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.Lottie> GetLottieAsync(int LottieId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_LottieRepository.GetLottie(LottieId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Get Attempt {LottieId} {ModuleId}", LottieId, ModuleId);
                return null;
            }
        }

        public Task<Models.Lottie> AddLottieAsync(Models.Lottie Lottie)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Lottie.ModuleId, PermissionNames.Edit))
            {
                Lottie = _LottieRepository.AddLottie(Lottie);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Lottie Added {Lottie}", Lottie);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Add Attempt {Lottie}", Lottie);
                Lottie = null;
            }
            return Task.FromResult(Lottie);
        }

        public Task<Models.Lottie> UpdateLottieAsync(Models.Lottie Lottie)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Lottie.ModuleId, PermissionNames.Edit))
            {
                Lottie = _LottieRepository.UpdateLottie(Lottie);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Lottie Updated {Lottie}", Lottie);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Update Attempt {Lottie}", Lottie);
                Lottie = null;
            }
            return Task.FromResult(Lottie);
        }

        public Task DeleteLottieAsync(int LottieId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _LottieRepository.DeleteLottie(LottieId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Lottie Deleted {LottieId}", LottieId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Delete Attempt {LottieId} {ModuleId}", LottieId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
