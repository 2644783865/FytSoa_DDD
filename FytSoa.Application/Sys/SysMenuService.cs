using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Model.Sys;
using FytSoa.Repository.Interfaces;
using FytSoa.Application.Sys.Dto;
using System.Linq.Expressions;
using FytSoa.Common.Extensions;
using System.Linq;

namespace FytSoa.Application.Sys
{
    public class SysMenuService : ISysMenuService
    {
        private readonly IBaseRepository<SysMenu> _thisRepository;
        public SysMenuService(IBaseRepository<SysMenu> thisRepository)
        {
            _thisRepository = thisRepository;
        }

        #region 查询列表，Tree
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenuTree>>> PageList(PageParam param)
        {
            var result = new ApiResult<List<SysMenuTree>>();
            try
            {
                Expression<Func<SysMenu, bool>> where = e => true;
                if (!string.IsNullOrEmpty(param.key))
                {
                    where = where.And(e => e.Name.Contains(param.key));
                }
                if (param.status != 0)
                {
                    where = where.And(e => e.Status == (param.status == 1 ? true : false));
                }
                var list = new List<SysMenuTree>();
                //查询所有
                var reslist = await _thisRepository.GetListAsync(where, m => m.Layer, 2);
                //转换到dto
                var listDto = reslist.MapToList<SysMenu, SysMenuTree>();
                foreach (var item in listDto.Where(m => m.ParentId == "0").OrderByDescending(m => m.Sort))
                {
                    item.children = GetChild(new List<SysMenuTree>(), listDto, item.Id);
                    list.Add(item);
                }
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 递归形成tree
        /// </summary>
        /// <returns></returns>
        private List<SysMenuTree> GetChild(List<SysMenuTree> resList, List<SysMenuTree> sourceList, string id)
        {
            var child = sourceList.Where(m => m.ParentId == id).OrderByDescending(m => m.Sort).ToList();
            foreach (var item in child)
            {
                item.children = GetChild(new List<SysMenuTree>(), sourceList, item.Id);
                resList.Add(item);
            }
            return resList;
        }
        #endregion

        #region 查询级联选择

        /// <summary>
        /// 级联选择列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenuSelect>>> GetSelect()
        {
            var result = new ApiResult<List<SysMenuSelect>>();
            try
            {
                var list = new List<SysMenuSelect>();
                //查询所有
                var reslist = await _thisRepository.GetListAsync();
                foreach (var item in reslist.Where(m => m.ParentId == "0").OrderByDescending(m => m.Sort))
                {
                    var selectModel = new SysMenuSelect()
                    {
                        value = item.Id.ToString(),
                        label = item.Name
                    };
                    var child = GetChildSelect(new List<SysMenuSelect>(), reslist, item.Id);
                    selectModel.children = child.Count > 0 ? child : null;
                    list.Add(selectModel);
                }
                result.Data = list;
            }
            catch (Exception ex)
            {
                result.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 递归形成tree
        /// </summary>
        /// <returns></returns>
        private List<SysMenuSelect> GetChildSelect(List<SysMenuSelect> resList, List<SysMenu> sourceList, string id)
        {
            var child = sourceList.Where(m => m.ParentId == id).OrderByDescending(m => m.Sort).ToList();
            foreach (var item in child)
            {
                var selectModel = new SysMenuSelect()
                {
                    value = item.Id.ToString(),
                    label = item.Name
                };
                var childs = GetChildSelect(new List<SysMenuSelect>(), sourceList, item.Id);
                selectModel.children = childs.Count > 0 ? childs : null;
                resList.Add(selectModel);
            }
            return resList;
        }
        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Add(SysMenuParam model)
        {
            var result = new ApiResult<string>();
            try
            {
                model.Id = Unique.Id();
                var menuModel = model.MapTo<SysMenuParam, SysMenu>(m => m.Id);
                if (model.Parent.Any())
                {
                    menuModel.ParentId = model.Parent[model.Parent.Count - 1];
                    menuModel.ParentGroupId = string.Join(",", model.Parent.ToArray()) + "," + menuModel.Id;
                    //查询，上级的层级
                    var paramModel =await _thisRepository.GetModelAsync(m=>m.Id==menuModel.ParentId);
                    if (paramModel!=null && !string.IsNullOrEmpty(paramModel.Id))
                    {
                        menuModel.Layer = paramModel.Layer + 1;
                    }
                }
                menuModel.CreateUser = "admin";
                await _thisRepository.AddAsync(menuModel);
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
        public async Task<ApiResult<string>> Modify(SysMenuParam model)
        {
            var result = new ApiResult<string>();
            try
            {
                var menuModel = model.MapTo<SysMenuParam, SysMenu>(m => m.Id);
                if (model.Parent.Any())
                {
                    menuModel.ParentId = model.Parent[model.Parent.Count - 1];
                    menuModel.ParentGroupId = string.Join(",", model.Parent.ToArray()) + "," + menuModel.Id;
                    //查询，上级的层级
                    var paramModel = await _thisRepository.GetModelAsync(m => m.Id == menuModel.ParentId);
                    if (paramModel != null && !string.IsNullOrEmpty(paramModel.Id))
                    {
                        menuModel.Layer = paramModel.Layer + 1;
                    }
                }
                await _thisRepository.UpdateAsync(menuModel,m=>new { m.CreateUser,m.CreateTime,m.IsDel});
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
        public async Task<ApiResult<SysMenu>> GetModel(string id)
        {
            var result = new ApiResult<SysMenu>();
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
        public async Task<ApiResult<string>> Delete(List<string> ids)
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
