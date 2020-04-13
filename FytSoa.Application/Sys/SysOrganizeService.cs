using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FytSoa.Application.Sys.Dto;
using FytSoa.Common;
using FytSoa.Common.Extensions;
using FytSoa.Model.Sys;
using FytSoa.Repository.Interfaces;

namespace FytSoa.Application.Sys
{
    public class SysOrganizeService : ISysOrganizeService
    {
        private readonly IBaseRepository<SysOrganize> _thisRepository;
        public SysOrganizeService(IBaseRepository<SysOrganize> thisRepository)
        {
            _thisRepository = thisRepository;
        }

        #region 查询Tree列表
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<SysOrganizeTree>>> GetTree(PageParam param)
        {
            var result = new ApiResult<List<SysOrganizeTree>>();
            try
            {
                Expression<Func<SysOrganize, bool>> where = e => true;
                if (!string.IsNullOrEmpty(param.key))
                {
                    where = where.And(e => e.Name.Contains(param.key));
                }
                if (param.status!=0)
                {
                    where = where.And(e => e.Status==(param.status==1?true:false));
                }
                var list = new List<SysOrganizeTree>();
                //查询所有
                var reslist =await _thisRepository.GetListAsync(where,m=>m.Layer,2);
                //转换到dto
                var listDto = reslist.MapToList<SysOrganize,SysOrganizeTree>();
                foreach (var item in listDto.Where(m=>m.ParentId=="0").OrderByDescending(m=>m.Sort))
                {
                    item.children = GetChild(new List<SysOrganizeTree>(), listDto,item.Id);
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
        private List<SysOrganizeTree> GetChild(List<SysOrganizeTree> resList, List<SysOrganizeTree> sourceList,string id)
        {
            var child = sourceList.Where(m=>m.ParentId==id).OrderByDescending(m=>m.Sort).ToList();
            foreach (var item in child)
            {
                item.children = GetChild(new List<SysOrganizeTree>(), sourceList,item.Id);
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
        public async Task<ApiResult<List<SysOrganizeSelect>>> GetSelect()
        {
            var result = new ApiResult<List<SysOrganizeSelect>>();
            try
            {
                var list = new List<SysOrganizeSelect>();
                //查询所有
                var reslist = await _thisRepository.GetListAsync();
                foreach (var item in reslist.Where(m => m.ParentId == 0).OrderByDescending(m => m.Sort))
                {
                    var selectModel = new SysOrganizeSelect() { 
                        value=item.Id.ToString(),
                        label=item.Name
                    };
                    var child = GetChildSelect(new List<SysOrganizeSelect>(), reslist, item.Id);
                    selectModel.children = child.Count>0 ? child :null;
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
        private List<SysOrganizeSelect> GetChildSelect(List<SysOrganizeSelect> resList, List<SysOrganize> sourceList, long id)
        {
            var child = sourceList.Where(m => m.ParentId == id).OrderByDescending(m => m.Sort).ToList();
            foreach (var item in child)
            {
                var selectModel = new SysOrganizeSelect()
                {
                    value = item.Id.ToString(),
                    label = item.Name
                };
                var childs = GetChildSelect(new List<SysOrganizeSelect>(), sourceList, item.Id);
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
        public async Task<ApiResult<string>> Add(SysOrganizeParam model)
        {
            var result = new ApiResult<string>();
            try
            {
                var organizeModel = model.MapTo<SysOrganizeParam, SysOrganize>();
                organizeModel.Id = Unique.Id();
                organizeModel.ParentId = model.Parent.GetLong()[model.Parent.GetLong().Count-1];
                organizeModel.ParentIdList = string.Join(",",model.Parent.ToArray())+","+organizeModel.Id; 
                organizeModel.CreateUser = "admin";
                //根据父级查询等级
                var parentModel =await _thisRepository.GetModelAsync(m=>m.Id==organizeModel.ParentId);
                if (parentModel != null)
                {
                    organizeModel.Layer = parentModel.Layer + 1;
                }
                await _thisRepository.AddAsync(organizeModel);
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
        public async Task<ApiResult<string>> Modify(SysOrganizeParam model)
        {
            var result = new ApiResult<string>();
            try
            {
                var organizeModel = model.MapTo<SysOrganizeParam, SysOrganize>();
                organizeModel.ParentId = model.Parent.GetLong()[model.Parent.GetLong().Count - 1];
                organizeModel.ParentIdList = string.Join(",", model.Parent) + "," + organizeModel.Id;
                //根据父级查询等级
                var parentModel = await _thisRepository.GetModelAsync(m => m.Id == organizeModel.ParentId);
                if (parentModel != null)
                {
                    organizeModel.Layer = parentModel.Layer + 1;
                }
                await _thisRepository.UpdateAsync(organizeModel,m=>new { m.CreateUser,m.CreateTime,m.IsDel});
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
        public async Task<ApiResult<SysOrganize>> GetModel(long id)
        {
            var result = new ApiResult<SysOrganize>();
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
                await _thisRepository.DeleteAsync(m=>m.ParentIdList.Contains(ids[0]));
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
