using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Sys;
using FytSoa.Application.Sys.Dto;
using FytSoa.Common;
using FytSoa.Model.Sys;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("Any")]
    public class SysOrganizeController : ControllerBase
    {
        private readonly ISysOrganizeService _sysOrganizeService;
        public SysOrganizeController(ISysOrganizeService sysOrganizeService)
        {
            _sysOrganizeService= sysOrganizeService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<SysOrganizeTree>>> List([FromQuery] PageParam param) => await _sysOrganizeService.GetTree(param);

        /// <summary>
        /// 级联选择列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("select")]
        public async Task<ApiResult<List<SysOrganizeSelect>>> GetSelect() => await _sysOrganizeService.GetSelect();

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Add([FromBody] SysOrganizeParam model) => await _sysOrganizeService.Add(model);

        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<string>> Modify([FromBody] SysOrganizeParam model) => await _sysOrganizeService.Modify(model);

        /// <summary>
        /// 获得一条信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ApiResult<SysOrganize>> GetModel(string id) => await _sysOrganizeService.GetModel(id);

        /// <summary>
        /// 删除，支持多条
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete([FromBody] List<string> ids) => await _sysOrganizeService.Delete(ids);

    }
}
