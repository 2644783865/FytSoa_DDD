using FytSoa.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Builder
{
    public interface IBuilderTool
    {
        /// <summary>
        /// 生成实体-单个
        /// </summary>
        /// <returns></returns>
        ApiResult<string> CreateModel(CreateBuilder param);

        /// <summary>
        /// 生成应用服务-单个
        /// </summary>
        /// <returns></returns>
        ApiResult<string> CreateService(CreateBuilder param);

        /// <summary>
        /// 生成Api-单个
        /// </summary>
        /// <returns></returns>
        ApiResult<string> CreateApi(CreateBuilder param);
    }
}
