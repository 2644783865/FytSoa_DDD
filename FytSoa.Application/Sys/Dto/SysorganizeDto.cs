using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Application.Sys.Dto
{
    /// <summary>
    /// 组织机构提交内容
    /// </summary>
    public class SysOrganizeParam
    {
        public string Id { get; set; }

        /// <summary>
        /// 父节点集合
        /// <summary>
        public List<string> Parent { get; set; }

        /// <summary>
        /// 部门名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 状态
        /// <summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 部门负责人
        /// <summary>
        public string LeaderUser { get; set; }

        /// <summary>
        /// 联系电话
        /// <summary>
        public string LeaderMobile { get; set; }

        /// <summary>
        /// 联系邮箱
        /// <summary>
        public string LeaderEmail { get; set; }
    }

    /// <summary>
    /// 适用于级联选择器
    /// </summary>
    public class SysOrganizeSelect
    {
        /// <summary>
        /// 值，对应数据库的主键Id
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<SysOrganizeSelect> children { get; set; }
    }

    /// <summary>
    /// 组织机构树形结构模型-适用于表格
    /// </summary>
    public class SysOrganizeTree
    {
        public string Id { get; set; }

        /// <summary>
        /// 父节点
        /// <summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 父节点集合
        /// <summary>
        public string ParentIdList { get; set; }

        /// <summary>
        /// 部门名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 状态
        /// <summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 部门负责人
        /// <summary>
        public string LeaderUser { get; set; }

        /// <summary>
        /// 部门负责人
        /// <summary>
        public string LeaderMobile { get; set; }

        /// <summary>
        /// 部门负责人
        /// <summary>
        public string LeaderEmail { get; set; }

        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<SysOrganizeTree> children { get; set; }
    }
}
