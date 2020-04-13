using FytSoa.Common;
using FytSoa.Common.Extensions;
using FytSoa.SugarCore;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Builder
{
    public class BuilderTool : IBuilderTool
    {
        public SqlSugarClient Db;
        public BuilderTool() {
            Db = new DbContext(FytSoaConfig.MySqlConnectionString,DbType.MySql).Db;
        }

        /// <summary>
        /// 生成接口
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiResult<string> CreateApi(CreateBuilder param)
        {
            var result = new ApiResult<string>() { StatusCode = (int)HttpStatusCode.InternalServerError };
            try
            {
                if (string.IsNullOrEmpty(param.DbTableName))
                {
                    result.Message = "数据库表名不能为空~";
                    return result;
                }
                //读取模板
                var strTemp = FileHelper.ReadFile("/Template/ApiController.html");
                strTemp = strTemp
                   .Replace("{NameSpace}", param.Namespace)
                   .Replace("{DataTable}", param.DbTableName)
                   .Replace("{TableName}", param.ModelName)
                   .Replace("{SmallName}", param.ModelName.FirstCharToLower());
                //写入文件
                FileHelper.WriteFile("/upload/temp/Controllers/" + param.Namespace + "/",  param.ModelName + "Controller.cs", strTemp);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 生成模型
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiResult<string> CreateModel(CreateBuilder param)
        {
            var result = new ApiResult<string>() { StatusCode=(int)HttpStatusCode.InternalServerError};
            try
            {
                if (string.IsNullOrEmpty(param.DbTableName))
                {
                    result.Message = "数据库表名不能为空~";
                    return result;
                }
                var column = Db.DbMaintenance.GetColumnInfosByTableName(param.DbTableName);
                //读取模板
                var strTemp = FileHelper.ReadFile("/Template/Model.html");
               

                //构建属性
                var attrStr = "";
                foreach (var item in column)
                {
                    attrStr += "        /// <summary>\r\n";
                    attrStr += "        /// " + item.ColumnDescription+ "\r\n";
                    attrStr += "        /// <summary>\r\n";
                    if (item.IsPrimarykey)
                    {
                        attrStr += "        [SugarColumn(IsPrimaryKey = true)]\r\n";
                    }
                    attrStr += "        public "+item.DataType.ConvertModelType() + " "+item.DbColumnName+ " { get; set; }"+item.DataType.ModelDefaultValue(item.DefaultValue)+"\r\n\r\n";
                }
                strTemp = strTemp
                   .Replace("{NameSpace}", param.Namespace)
                   .Replace("{DataTable}", param.DbTableName)
                   .Replace("{TableName}", param.ModelName)
                   .Replace("{AttributeList}", attrStr);
                //写入文件
                FileHelper.WriteFile("/upload/temp/Model/" + param.Namespace+"/",param.ModelName+".cs",strTemp);
                result.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 生成服务于实现
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ApiResult<string> CreateService(CreateBuilder param)
        {
            var result = new ApiResult<string>() { StatusCode = (int)HttpStatusCode.InternalServerError };
            try
            {
                if (string.IsNullOrEmpty(param.DbTableName))
                {
                    result.Message = "数据库表名不能为空~";
                    return result;
                }
                //读取模板
                var strTemp = FileHelper.ReadFile("/Template/IService.html");
                strTemp = strTemp
                   .Replace("{NameSpace}", param.Namespace)
                   .Replace("{DataTable}", param.DbTableName)
                   .Replace("{TableName}", param.ModelName);
                //写入文件
                FileHelper.WriteFile("/upload/temp/Application/" + param.Namespace + "/", "I"+param.ModelName + "Service.cs", strTemp);

                //读取模板
                var strServiceTemp = FileHelper.ReadFile("/Template/Service.html");
                strServiceTemp = strServiceTemp
                   .Replace("{NameSpace}", param.Namespace)
                   .Replace("{DataTable}", param.DbTableName)
                   .Replace("{TableName}", param.ModelName);
                //写入文件
                FileHelper.WriteFile("/upload/temp/Application/" + param.Namespace + "/", param.ModelName + "Service.cs", strServiceTemp);
                result.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
