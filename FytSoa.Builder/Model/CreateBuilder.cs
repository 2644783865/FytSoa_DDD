using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Builder
{
    /// <summary>
    /// 生成的对象
    /// </summary>
    public class CreateBuilder
    {
        /// <summary>
        /// 数据库表名字  例如：sys_admin
        /// </summary>
        public string DbTableName { get; set; }

        /// <summary>
        /// 实体名字   例如：SysAdmin
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 命名空间，根据不同的业务，分文件夹=命名空间
        /// </summary>
        public string Namespace { get; set; }
    }
}
