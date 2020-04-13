using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FytSoa.Builder;
using FytSoa.Common;

namespace FytSoa.Application.Builder
{
    public interface IBuilderService
    {
        /// <summary>
        /// 生成实体、服务、Api
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiResult<string>> CreateCode(CreateBuilder model);
    }
}
