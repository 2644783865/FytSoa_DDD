using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Application.Builder;
using FytSoa.Builder;
using FytSoa.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuilderController : ControllerBase
    {
        private readonly IBuilderService _builderService;
        public BuilderController(IBuilderService builderService)
        {
            _builderService = builderService;
        }

        [HttpGet]
        public async Task<ApiResult<string>> Get([FromQuery] CreateBuilder param) 
            => await _builderService.CreateCode(param);
    }
}