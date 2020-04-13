using FytSoa.Common;
using FytSoa.SugarCore;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Repository.Implements
{
    public class BaseService
    {
        protected SqlSugarClient Db;

        public BaseService()
        {
            Db = new DbContext(FytSoaConfig.MySqlConnectionString, DbType.MySql).Db;
        }
    }
}
