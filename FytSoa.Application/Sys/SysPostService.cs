using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Model.Sys;
using FytSoa.Repository.Interfaces;

namespace FytSoa.Application.Sys
{
    public class SysPostService : ISysPostService
    {
        private readonly IBaseRepository<SysPost> _thisRepository;
        public SysPostService(IBaseRepository<SysPost> thisRepository)
        {
            _thisRepository = thisRepository;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<SysPost>>> PageList(PageParam param)
        {
            var result = new ApiResult<Page<SysPost>>();
            try
            {
                result.Data = await _thisRepository.GetPagesAsync(param.page,param.limit);
            }
            catch (Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Add(SysPost model)
        {
            var result = new ApiResult<string>();
            try
            {
                await _thisRepository.AddAsync(model);
            }
            catch (Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Modify(SysPost model)
        {
            var result = new ApiResult<string>();
            try
            {
                await _thisRepository.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysPost>> GetModel(long id)
        {
            var result = new ApiResult<SysPost>();
            try
            {
                await _thisRepository.GetModelAsync(m=>m.Id==id);
            }
            catch (Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Delete(List<long> ids)
        {
            var result = new ApiResult<string>();
            try
            {
                await _thisRepository.DeleteAsync(m=>ids.Contains(m.Id));
            }
            catch (Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
