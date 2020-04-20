using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Sys;
using FytSoa.Builder;
using FytSoa.Common;
using FytSoa.Model.Sys;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISysAdminService _adminService;
        private readonly IBuilderTool _toolService;
        public WeatherForecastController(ISysAdminService adminService
            , IBuilderTool toolService)
        {
            _adminService= adminService;
            _toolService = toolService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var model = new Builder.Builder() { DbTableName = "sys_organize", ModelName = "SysOrganize", Namespace = "Sys" };
        //    await Task.Run(()=> _toolService.CreateModel(model));
        //    await Task.Run(()=> _toolService.CreateService(model));
        //    await Task.Run(() => _toolService.CreateApi(model));
        //    return Ok("Ok");
        //}

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<Page<SysAdmin>>> List([FromBody] PageParam param) => await _adminService.PageList(param);

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Add([FromBody] SysAdmin model) => await _adminService.Add(model);

        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<string>> Modify([FromBody] SysAdmin model) => await _adminService.Modify(model);

        /// <summary>
        /// 删除，支持多条
        /// </summary>
        /// <param name="ids">List</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete([FromBody] List<string> ids) => await _adminService.Delete(ids);

    }
}
