using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Application.Sys.Dto
{
    public class SysMenuParam
    {
        public string Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 父节点集合
        /// <summary>
        public List<string> Parent { get; set; }

        /// <summary>
        /// 权限标识
        /// <summary>
        public string Code { get; set; }


        /// <summary>
        /// 路由地址
        /// <summary>
        public string Urls { get; set; }

        /// <summary>
        /// 菜单图标
        /// <summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 状态
        /// <summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 菜单类型：1=目录  2=菜单
        /// <summary>
        public int Types { get; set; } = 1;

        /// <summary>
        /// 菜单按钮
        /// <summary>
        public string BtnFun { get; set; }

        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 适用于级联选择器
    /// </summary>
    public class SysMenuSelect
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
        public List<SysMenuSelect> children { get; set; }
    }

    /// <summary>
    /// 返回前端列表，可用于修改添加列表
    /// </summary>
    public class SysMenuTree
    {
        public string Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// <summary>
        public string Name { get; set; }

        /// <summary>
        /// 父节点
        /// <summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 父节点集合组
        /// <summary>
        public string ParentGroupId { get; set; }

        /// <summary>
        /// 权限标识
        /// <summary>
        public string Code { get; set; }


        /// <summary>
        /// 路由地址
        /// <summary>
        public string Urls { get; set; }

        /// <summary>
        /// 菜单图标
        /// <summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// <summary>
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 状态
        /// <summary>
        public bool Status { get; set; } = true;

        /// <summary>
        /// 菜单类型：1=目录  2=菜单
        /// <summary>
        public int Types { get; set; } = 1;

        /// <summary>
        /// 菜单按钮
        /// <summary>
        public string BtnFun { get; set; }

        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<SysMenuTree> children { get; set; }
    }
}
