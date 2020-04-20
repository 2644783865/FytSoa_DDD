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
    public class SysMenuController : ControllerBase
    {
        private readonly ISysMenuService _sysMenuService;
        public SysMenuController(ISysMenuService sysMenuService)
        {
            _sysMenuService= sysMenuService;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<SysMenuTree>>> List([FromQuery] PageParam param) => await _sysMenuService.PageList(param);

        /// <summary>
        /// 级联选择列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("select")]
        public async Task<ApiResult<List<SysMenuSelect>>> GetSelect() => await _sysMenuService.GetSelect();

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Add([FromBody] SysMenuParam model) => await _sysMenuService.Add(model);

        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<string>> Modify([FromBody] SysMenuParam model) => await _sysMenuService.Modify(model);

        /// <summary>
        /// 获得一条信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ApiResult<SysMenu>> GetModel(string id) => await _sysMenuService.GetModel(id);

        /// <summary>
        /// 删除，支持多条
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete([FromBody] List<string> ids) => await _sysMenuService.Delete(ids);

    }
}
