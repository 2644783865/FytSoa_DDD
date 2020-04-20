using FytSoa.Application.Sys.Dto;
using FytSoa.Common;
using FytSoa.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Application.Sys
{
    public interface ISysMenuService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<List<SysMenuTree>>> PageList(PageParam param);

        /// <summary>
        /// 级联选择列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<List<SysMenuSelect>>> GetSelect();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Add(SysMenuParam model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Modify(SysMenuParam model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<SysMenu>> GetModel(string id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Delete(List<string> ids);
    }
}