using FytSoa.Builder;
using FytSoa.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Application.Builder
{
    public class BuilderService : IBuilderService
    {
        private readonly IBuilderTool _toolService;
        public BuilderService(IBuilderTool toolService)
        {
            _toolService = toolService;
        }

        /// <summary>
        /// 生成实体、服务、Api
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateCode(CreateBuilder model)
        {
            await Task.Run(()=> _toolService.CreateModel(model));
            await Task.Run(() => _toolService.CreateService(model));
            await Task.Run(() => _toolService.CreateApi(model));
            return new ApiResult<string>();
        }
    }
}
