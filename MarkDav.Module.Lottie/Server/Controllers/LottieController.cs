using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using MarkDav.Module.Lottie.Repository;
using Oqtane.Controllers;
using System.Net;

namespace MarkDav.Module.Lottie.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class LottieController : ModuleControllerBase
    {
        private readonly ILottieRepository _LottieRepository;

        public LottieController(ILottieRepository LottieRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _LottieRepository = LottieRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Lottie> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _LottieRepository.GetLotties(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Lottie Get(int id)
        {
            Models.Lottie Lottie = _LottieRepository.GetLottie(id);
            if (Lottie != null && IsAuthorizedEntityId(EntityNames.Module, Lottie.ModuleId))
            {
                return Lottie;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Get Attempt {LottieId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Lottie Post([FromBody] Models.Lottie Lottie)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, Lottie.ModuleId))
            {
                Lottie = _LottieRepository.AddLottie(Lottie);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Lottie Added {Lottie}", Lottie);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Post Attempt {Lottie}", Lottie);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Lottie = null;
            }
            return Lottie;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Lottie Put(int id, [FromBody] Models.Lottie Lottie)
        {
            if (ModelState.IsValid && Lottie.LottieId == id && IsAuthorizedEntityId(EntityNames.Module, Lottie.ModuleId) && _LottieRepository.GetLottie(Lottie.LottieId, false) != null)
            {
                Lottie = _LottieRepository.UpdateLottie(Lottie);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Lottie Updated {Lottie}", Lottie);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Put Attempt {Lottie}", Lottie);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Lottie = null;
            }
            return Lottie;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Lottie Lottie = _LottieRepository.GetLottie(id);
            if (Lottie != null && IsAuthorizedEntityId(EntityNames.Module, Lottie.ModuleId))
            {
                _LottieRepository.DeleteLottie(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Lottie Deleted {LottieId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Lottie Delete Attempt {LottieId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
