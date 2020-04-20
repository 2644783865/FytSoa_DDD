using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Sys;
using FytSoa.Application.Sys.Dto;
using FytSoa.Common;
using FytSoa.Model.Sys;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SysRoleController : ControllerBase
    {
        private readonly ISysRoleService _sysRoleService;
        public SysRoleController(ISysRoleService sysRoleService)
        {
            _sysRoleService= sysRoleService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<Page<SysRole>>> List([FromQuery] PageParam param) => await _sysRoleService.PageList(param);

        /// <summary>
        /// 查询用户组
        /// </summary>
        /// <returns></returns>
        [HttpGet("group")]
        public async Task<ApiResult<List<SysRoleGroupDto>>> GetGroup() => await _sysRoleService.GroupSelect();

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Add([FromBody] SysRole model) => await _sysRoleService.Add(model);

        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<string>> Modify([FromBody] SysRole model) => await _sysRoleService.Modify(model);

        /// <summary>
        /// 获得一条信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ApiResult<SysRole>> GetModel(string id) => await _sysRoleService.GetModel(id);

        /// <summary>
        /// 删除，支持多条
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete([FromBody] List<string> ids) => await _sysRoleService.Delete(ids);

    }
}
