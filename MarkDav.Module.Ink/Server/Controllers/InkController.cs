using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using MarkDav.Module.Ink.Repository;
using Oqtane.Controllers;
using System.Net;

namespace MarkDav.Module.Ink.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class InkController : ModuleControllerBase
    {
        private readonly IInkRepository _InkRepository;

        public InkController(IInkRepository InkRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _InkRepository = InkRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Ink> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _InkRepository.GetInks(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Ink Get(int id)
        {
            Models.Ink Ink = _InkRepository.GetInk(id);
            if (Ink != null && IsAuthorizedEntityId(EntityNames.Module, Ink.ModuleId))
            {
                return Ink;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Get Attempt {InkId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Ink Post([FromBody] Models.Ink Ink)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, Ink.ModuleId))
            {
                Ink = _InkRepository.AddInk(Ink);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Ink Added {Ink}", Ink);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Post Attempt {Ink}", Ink);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Ink = null;
            }
            return Ink;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Ink Put(int id, [FromBody] Models.Ink Ink)
        {
            if (ModelState.IsValid && Ink.InkId == id && IsAuthorizedEntityId(EntityNames.Module, Ink.ModuleId) && _InkRepository.GetInk(Ink.InkId, false) != null)
            {
                Ink = _InkRepository.UpdateInk(Ink);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Ink Updated {Ink}", Ink);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Put Attempt {Ink}", Ink);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Ink = null;
            }
            return Ink;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Ink Ink = _InkRepository.GetInk(id);
            if (Ink != null && IsAuthorizedEntityId(EntityNames.Module, Ink.ModuleId))
            {
                _InkRepository.DeleteInk(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Ink Deleted {InkId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ink Delete Attempt {InkId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
