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
    public class SysCodeController : ControllerBase
    {
        private readonly ISysCodeService _sysCodeService;
        public SysCodeController(ISysCodeService sysCodeService)
        {
            _sysCodeService= sysCodeService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<Page<SysCode>>> List([FromQuery] PageParam param) => await _sysCodeService.PageList(param);

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Add([FromBody] SysCode model) => await _sysCodeService.Add(model);

        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<string>> Modify([FromBody] SysCode model) => await _sysCodeService.Modify(model);

        /// <summary>
        /// 获得一条信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ApiResult<SysCode>> GetModel(string id) => await _sysCodeService.GetModel(id);

        /// <summary>
        /// 删除，支持多条
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete([FromBody] List<string> ids) => await _sysCodeService.Delete(ids);

    }
}
