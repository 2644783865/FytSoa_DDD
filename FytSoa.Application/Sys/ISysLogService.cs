using FytSoa.Common;
using FytSoa.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Application.Sys
{
    public interface ISysLogService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<Page<SysLog>>> PageList(PageParam param);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Add(SysLog model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Modify(SysLog model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<SysLog>> GetModel(string id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Delete(List<string> ids);
    }
}