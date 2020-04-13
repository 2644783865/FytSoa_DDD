using FytSoa.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.SugarCore
{
    /// <summary>
    /// 负责多表查询使用
    /// </summary>
    public class MyContext
    {
        protected SqlSugarClient Db;

        public MyContext()
        {
            Db = new DbContext(FytSoaConfig.MySqlConnectionString, DbType.MySql).Db;
        }
    }
}
