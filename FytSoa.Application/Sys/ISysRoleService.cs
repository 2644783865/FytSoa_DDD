using FytSoa.Application.Sys.Dto;
using FytSoa.Common;
using FytSoa.Model.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Application.Sys
{
    public interface ISysRoleService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ApiResult<Page<SysRole>>> PageList(PageParam param);

        /// <summary>
        /// 查询角色组
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysRoleGroupDto>>> GroupSelect();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Add(SysRole model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Modify(SysRole model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<SysRole>> GetModel(string id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Delete(List<string> ids);
    }
}