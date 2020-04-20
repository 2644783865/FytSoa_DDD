using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Sys;
using FytSoa.Common;
using FytSoa.Model.Sys;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SysAuthorityController : ControllerBase
    {
        private readonly ISysAuthorityService _sysAuthorityService;
        public SysAuthorityController(ISysAuthorityService sysAuthorityService)
        {
            _sysAuthorityService= sysAuthorityService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<Page<SysAuthority>>> List([FromQuery] PageParam param) => await _sysAuthorityService.PageList(param);

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Add([FromBody] SysAuthority model) => await _sysAuthorityService.Add(model);

        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<string>> Modify([FromBody] SysAuthority model) => await _sysAuthorityService.Modify(model);


        /// <summary>
        /// 删除，支持多条
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete([FromBody] List<string> ids) => await _sysAuthorityService.Delete(ids);

    }
}
