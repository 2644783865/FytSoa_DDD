using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace FytSoa.Common.Extensions
{
    public static class StringExtension
    {
        public static bool _windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static string ReplacePath(this string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";
            if (_windows)
                return path.Replace("/", "\\");
            return path.Replace("\\", "/");

        }
        private static DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);

        private static long longTime = 621355968000000000;

        private static int samllTime = 10000000;
        /// <summary>
        /// 获取时间戳 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStamp(this DateTime dateTime)
        {
            return (dateTime.ToUniversalTime().Ticks - longTime) / samllTime;
        }
        /// <summary>
        /// 时间戳转换成日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetTimeSpmpToDate(this object timeStamp)
        {
            if (timeStamp == null) return dateStart;
            DateTime dateTime = new DateTime(longTime + Convert.ToInt64(timeStamp) * samllTime, DateTimeKind.Utc).ToLocalTime();
            return dateTime;
        }
       

        public static bool IsUrl(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            string Url = @"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            return Regex.IsMatch(str, Url);

        }
        /// <summary>
        /// 判断是不是正确的手机号码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhoneNo(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            if (input.Length != 11)
                return false;

            if (new Regex(@"^1[3578][01379]\d{8}$").IsMatch(input)
                || new Regex(@"^1[34578][01256]\d{8}").IsMatch(input)
                || new Regex(@"^(1[012345678]\d{8}|1[345678][0123456789]\d{8})$").IsMatch(input)
                )
                return true;
            return false;
        }


        public static bool GetGuid(this string guid, out Guid outId)
        {
            Guid emptyId = Guid.Empty;
            return Guid.TryParse(guid, out outId);
        }

        public static bool IsGuid(this string guid)
        {
            Guid newId;
            return guid.GetGuid(out newId);
        }

        public static bool IsInt(this object obj)
        {
            if (obj == null)
                return false;
            bool reslut = Int32.TryParse(obj.ToString(), out int _number);
            return reslut;

        }
        public static bool IsDate(this object str)
        {
            return str.IsDate(out _);
        }
        public static bool IsDate(this object str, out DateTime dateTime)
        {
            dateTime = DateTime.Now;
            if (str == null || str.ToString() == "")
            {
                return false;
            }
            return DateTime.TryParse(str.ToString(), out dateTime);
        }
        /// <summary>
        /// 根据传入格式判断是否为小数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="formatString">18,5</param>
        /// <returns></returns>
        public static bool IsNumber(this string str, string formatString)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            int precision = 32;
            int scale = 5;
            try
            {
                if (string.IsNullOrEmpty(formatString))
                {
                    precision = 10;
                    scale = 2;
                }
                else
                {
                    string[] numbers = formatString.Split(',');
                    precision = Convert.ToInt32(numbers[0]);
                    scale = numbers.Length == 0 ? 2 : Convert.ToInt32(numbers[1]);
                }
            }
            catch { };
            return IsNumber(str, precision, scale);
        }
        /**/
        /// <summary>
        /// 判断一个字符串是否为合法数字(指定整数位数和小数位数)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="precision">整数位数</param>
        /// <param name="scale">小数位数</param>
        /// <returns></returns>
        public static bool IsNumber(this string str, int precision, int scale)
        {
            if ((precision == 0) && (scale == 0))
            {
                return false;
            }
            string pattern = @"(^\d{1," + precision + "}";
            if (scale > 0)
            {
                pattern += @"\.\d{0," + scale + "}$)|" + pattern;
            }
            pattern += "$)";
            return Regex.IsMatch(str, pattern);
        }


        public static int GetInt(this object obj)
        {
            if (obj == null)
                return 0;
            int.TryParse(obj.ToString(), out int _number);
            return _number;

        }

        /// <summary>
        /// 获取 object 中的枚举值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static long GetLong(this object obj)
        {
            if (obj == null)
                return 0;

            try
            {
                return Convert.ToInt64(Convert.ToDouble(obj));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 字符串数组转Long数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static List<long> GetLong(this List<string> obj)
        {
            var list = new List<long>();
            if (obj == null)
                return list;
            try
            {
                return obj.ConvertAll(m=>Convert.ToInt64(m));
            }
            catch
            {
                return list;
            }
        }

        /// <summary>
        /// 获取 object 中的 float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static float GetFloat(this object obj)
        {
            if (System.DBNull.Value.Equals(obj) || null == obj)
                return 0;

            try
            {
                return float.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static double GetDouble(this object obj)
        {
            if (System.DBNull.Value.Equals(obj) || null == obj)
                return 0;

            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取 object 中的 decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static decimal GetDecimal(this object obj)
        {
            if (System.DBNull.Value.Equals(obj) || null == obj)
                return 0;

            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取 object 中的 decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static dynamic GetDynamic(this object obj)
        {
            if (System.DBNull.Value.Equals(obj) || null == obj)
                return null;

            try
            {
                string str = obj.ToString();
                if (str.IsNumber(25, 15)) return Convert.ToDecimal(obj);
                else return str;
            }
            catch
            {
                return string.Empty;
            }
        }


        private static char[] randomConstant ={
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
      };
        /// <summary>
        /// 生成指定长度的随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber(this int length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(randomConstant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        #region 数据库字段转换兼容，以及附加默认值

        /// <summary>
        /// 数据库类型，转换成实体类型
        /// </summary>
        /// <param name="DbType"></param>
        /// <returns></returns>
        public static string ConvertModelType(this string DbType)
        {
            return (DbType.ToLower()) switch
            {
                "varchar" => "string",
                "text" => "string",
                "longtext" => "string",
                "bit" => "bool",
                "bigint" => "long",
                "datetime" => "DateTime",
                "timestamp" => "DateTime",
                _ => DbType,
            };
        }

        /// <summary>
        /// 数据库类型，转换成实体类型
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string ModelDefaultValue(this string dbType, string defaultValue)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(defaultValue))
            {
                return str;
            }
            return (dbType.ToLower()) switch
            {
                "int" => " = "+defaultValue+";",
                "bit" => " = "+(defaultValue=="b'0'"?"false":"true")+";",
                _ => str,
            };
        }
        #endregion


        /// <summary>
        /// 首字母小写写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToLower(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToLower() + input.Substring(1);
            return str;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToUpper() + input.Substring(1);
            return str;
        }
    }
}
